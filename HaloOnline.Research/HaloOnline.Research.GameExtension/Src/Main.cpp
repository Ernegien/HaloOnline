#include <windows.h>

bool __stdcall DllMain(void* p_Module, unsigned long p_Reason, void* p_Reserved)
{
	// TODO: have argument to wait for attached debugger before continuing
	abort();

	return true;
}