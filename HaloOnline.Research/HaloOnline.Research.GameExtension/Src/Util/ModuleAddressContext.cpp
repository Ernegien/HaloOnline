#include "ModuleAddressContext.hpp"

ModuleAddressContext::ModuleAddressContext() { }

ModuleAddressContext::ModuleAddressContext(const uint32_t imageBaseAddress, const uint32_t baseAddress, const uint32_t size)
{
	ImageBaseAddress = imageBaseAddress;
	BaseAddress = baseAddress;
	Size = size;
}