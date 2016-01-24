#include <windows.h>
#include "Util/ProcessAddress.h"
#include <TlHelp32.h>
#include <fstream>

void InitializeProcessAddress()
{
	// get process base address
	HMODULE module = GetModuleHandle(NULL);

	// get loaded module's file path
	WCHAR modulePath[MAX_PATH + 1] = { 0 };
	GetModuleFileName(module, modulePath, _countof(modulePath));

	// grab the original image base address from the PE header
	uint32_t imageBaseAddress;
	IMAGE_DOS_HEADER dosHeader = { 0 };
	std::ifstream fs(modulePath, std::ios::in | std::ios::binary);
	fs.read(reinterpret_cast<char*>(&dosHeader), sizeof(IMAGE_DOS_HEADER));
	uint32_t peHeaderOffset = dosHeader.e_lfanew;
	fs.seekg(peHeaderOffset + offsetof(IMAGE_NT_HEADERS, OptionalHeader) + offsetof(IMAGE_OPTIONAL_HEADER32, ImageBase), fs.beg);
	fs.read(reinterpret_cast<char*>(&imageBaseAddress), sizeof(uint32_t));
	fs.close();

	ProcessAddress::Initialize(imageBaseAddress, reinterpret_cast<uint32_t>(module));
}


bool __stdcall DllMain(void* p_Module, unsigned long p_Reason, void* p_Reserved)
{
	// TODO: have argument to wait indefinitely for attached debugger before continuing
	Sleep(15000);

	InitializeProcessAddress();

	uint32_t imageAddress = ProcessAddress::ToImageAddress(0x83575000);
	uint32_t processAddress = ProcessAddress::FromImageAddress(0x4002000);

	ProcessAddress imageAddress2 = ProcessAddress::ToImageAddress(0x83575000);
	ProcessAddress processAddress2 = ProcessAddress::FromImageAddress(0x4002000);

	ProcessAddress processAddress3 = ProcessAddress(0x83575000);
	ProcessAddress processAddress4(0x83575000);

	imageAddress = processAddress3.ToImageAddress();

	uint32_t unboxed = processAddress3;
	ProcessAddress boxed = unboxed;


	return true;
}