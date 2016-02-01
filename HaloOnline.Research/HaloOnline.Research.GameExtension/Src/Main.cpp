#include <windows.h>
#include "Util/DefaultDictionary.hpp"
#include <fstream>
#include <unordered_map>
#include <Psapi.h>
#include "Util/ModuleAddress.hpp"
#include "Util/EngineVersion.hpp"
#include "Util/Patch.hpp"
#include "Util/ModificationSet.hpp"

// TODO: globalize these elsewhere
ModuleAddressContext MainModuleContext;
EngineVersion Version;

void GatherModuleInfo()
{
	MODULEINFO moduleInfo = { 0 };
	if (!GetModuleInformation(GetCurrentProcess(), GetModuleHandle(nullptr), &moduleInfo, sizeof(moduleInfo)))
	{
		throw std::exception("Unable to retrieve module information.");
	}
	
	// get loaded module's file path
	WCHAR modulePath[MAX_PATH + 1] = { 0 };
	GetModuleFileName(GetModuleHandle(nullptr), modulePath, _countof(modulePath));

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

	Version = static_cast<EngineVersion>(imageTimestamp);
	MainModuleContext = ModuleAddressContext(imageBaseAddress, reinterpret_cast<uint32_t>(moduleInfo.lpBaseOfDll), moduleInfo.SizeOfImage);
}



LONG WINAPI CustomExceptionFilter(EXCEPTION_POINTERS *exceptionInfo)
{
	MessageBoxA(nullptr, std::string("EIP: 0x%x", exceptionInfo->ContextRecord->Eip).c_str(), "Game Crash", MB_OK);

	std::ofstream logFile;
	logFile.open("exception.log", std::ios::out | std::ios::ate);
	logFile << std::string("EIP: 0x%x\r\n", exceptionInfo->ContextRecord->Eip).c_str();
	logFile.close();

	return EXCEPTION_CONTINUE_SEARCH;
}

typedef DefaultDictionary<EngineVersion, ModuleAddress> EngineAddressMap;
typedef DefaultDictionary<EngineVersion, ModificationSet> EngineModificationSetMap;

bool __stdcall DllMain(void* p_Module, unsigned long p_Reason, void* p_Reserved)
{
	// TODO: have argument to wait indefinitely for attached debugger before continuing
	Sleep(15000);

	GatherModuleInfo();

	// custom
	EngineAddressMap fmodPatchAddress2(Version,
	{
		{ Alpha, ModuleAddress::FromImageAddress(MainModuleContext, 0x140DA75) },
		{ Latest, ModuleAddress::FromImageAddress(MainModuleContext, 0xFAA9E5) }
	});

	ModuleAddress patchAddr = fmodPatchAddress2;

	ModificationSet({
		Patch::Create(patchAddr, { 0x12, 0x53, 0x95 }),
		Patch::Create(patchAddr, { 0x0 })
	}).Apply();

	auto set1 = ModificationSet({
		Patch::Create(patchAddr,{ 0x12, 0x53, 0x95 }),
		Patch::Create(patchAddr,{ 0x0 })
	});
	set1.Toggle();
	set1.Toggle();

	auto set2 = ModificationSet({
		Patch::Create(patchAddr,{ 0x12, 0x53, 0x95 }),
		Patch::Create(patchAddr,{ 0x0 })
	});

	//ModificationSet({
	//	set1,
	//	set2
	//}).Apply();



	Patch testpatch = Patch(patchAddr, { 0x00, 0x01, 0x2, 0x3 });
	testpatch.Apply();


	return true;
}