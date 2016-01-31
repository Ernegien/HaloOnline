#include <windows.h>
#include "Util/ProcessAddress.hpp"
#include "Util/DefaultDictionary.hpp"
#include <fstream>
#include <unordered_map>

void InitializeProcessAddress()
{
	// get process base address
	HMODULE module = GetModuleHandle(nullptr);

	// get loaded module's file path
	WCHAR modulePath[MAX_PATH + 1] = { 0 };
	GetModuleFileName(module, modulePath, _countof(modulePath));

	// grab the original image base address and build timestamp from the PE header
	uint32_t imageBaseAddress, imageTimestamp;
	IMAGE_DOS_HEADER dosHeader = { 0 };
	std::ifstream fs(modulePath, std::ios::in | std::ios::binary);
	fs.read(reinterpret_cast<char*>(&dosHeader), sizeof(dosHeader));
	fs.seekg(dosHeader.e_lfanew + offsetof(IMAGE_NT_HEADERS, FileHeader) + offsetof(IMAGE_FILE_HEADER, TimeDateStamp));
	fs.read(reinterpret_cast<char*>(&imageTimestamp), sizeof(imageTimestamp));
	fs.seekg(dosHeader.e_lfanew + offsetof(IMAGE_NT_HEADERS, OptionalHeader) + offsetof(IMAGE_OPTIONAL_HEADER32, ImageBase));
	fs.read(reinterpret_cast<char*>(&imageBaseAddress), sizeof(imageBaseAddress));
	fs.close();

	ProcessAddress::Initialize(imageBaseAddress, reinterpret_cast<uint32_t>(module));
}

// The engine version represented by the executable build timestamp
enum EngineVersion : uint32_t
{
	Alpha = 0x550c2dc5,
	Latest = 0x565493d6
};


LONG WINAPI CustomExceptionFilter(EXCEPTION_POINTERS *exceptionInfo)
{
	MessageBoxA(nullptr, std::string("EIP: 0x%x", exceptionInfo->ContextRecord->Eip).c_str(), "Game Crash", MB_OK);

	std::ofstream logFile;
	logFile.open("exception.log", std::ios::out | std::ios::ate);
	logFile << std::string("EIP: 0x%x\r\n", exceptionInfo->ContextRecord->Eip).c_str();
	logFile.close();

	return EXCEPTION_CONTINUE_SEARCH;
}

bool __stdcall DllMain(void* p_Module, unsigned long p_Reason, void* p_Reserved)
{
	// exception traces
	//AddVectoredExceptionHandler()

	// TODO: have argument to wait indefinitely for attached debugger before continuing
	Sleep(10000);

	//SetUnhandledExceptionFilter(nullptr);
	//SetUnhandledExceptionFilter(CustomExceptionFilter);

	//int* p1 = NULL;
	//*p1 = 0;

	InitializeProcessAddress();


	EngineVersion engineVersion = Latest;

	// vanilla
	const std::unordered_map<EngineVersion, ProcessAddress> fmodPatchAddress
	{
		{ Alpha, ProcessAddress::FromImageAddress(0x140DA75) },
		{ Latest, ProcessAddress::FromImageAddress(0xFAA9E5) }
	};

	ProcessAddress patchAddr = fmodPatchAddress.at(engineVersion);

	// custom
	DefaultDictionary<EngineVersion, ProcessAddress> fmodPatchAddress2(engineVersion,
	{
		{ Alpha, ProcessAddress::FromImageAddress(0x140DA75) },
		{ Latest, ProcessAddress::FromImageAddress(0xFAA9E5) }
	});

	ProcessAddress patchAddr2 = fmodPatchAddress2;



	return true;
}