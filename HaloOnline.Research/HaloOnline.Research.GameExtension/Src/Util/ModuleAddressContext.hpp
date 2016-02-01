#pragma once
#include <cstdint>

struct ModuleAddressContext
{
	uint32_t ImageBaseAddress;
	uint32_t BaseAddress;
	uint32_t Size;

	ModuleAddressContext();
	ModuleAddressContext(const uint32_t imageBaseAddress, const uint32_t baseAddress, const uint32_t size);
};