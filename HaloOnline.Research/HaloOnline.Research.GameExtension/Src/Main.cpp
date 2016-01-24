#include <windows.h>
#include <stdint.h>
#include "Util/ProcessAddress.h"

bool __stdcall DllMain(void* p_Module, unsigned long p_Reason, void* p_Reserved)
{
	// TODO: have argument to wait for attached debugger before continuing
	//abort();

	Sleep(15000);


	ProcessAddress::Initialize(0x4000000, 0x83573000);


	auto imageAddress = ProcessAddress::ToImageAddress(0x83575000);
	auto processAddress = ProcessAddress::FromImageAddress(0x4002000);

	ProcessAddress processAddress2 = ProcessAddress(0x83575000);
	ProcessAddress processAddress3(0x83575000);

	imageAddress = processAddress2.ToImageAddress();

	uint32_t unboxed = processAddress2;

	ProcessAddress boxed = unboxed;


	return true;
}