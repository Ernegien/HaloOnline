#pragma once
#include <stdint.h>
#include "ModuleAddressContext.hpp"

struct ModuleAddress
{
	const ModuleAddressContext Context;
	const uint32_t Value;

	ModuleAddress(const ModuleAddressContext &context, const uint32_t moduleAddress);
	~ModuleAddress();

	uint32_t ToImageAddress() const;
	operator uint32_t() const;

	static uint32_t ToImageAddress(const ModuleAddressContext &context, const uint32_t moduleAddress);
	static ModuleAddress FromImageAddress(const ModuleAddressContext &context, const uint32_t imageAddress);
	static bool IsValidModuleAddress(const ModuleAddressContext &context, const uint32_t moduleAddress);
};