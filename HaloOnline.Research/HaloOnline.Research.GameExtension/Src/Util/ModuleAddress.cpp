#include "ModuleAddress.hpp"
#include <stdexcept>

ModuleAddress::ModuleAddress(const ModuleAddressContext &context, const uint32_t moduleAddress) : Context(context), Value(moduleAddress)
{
	if (!IsValidModuleAddress(context, moduleAddress))
		throw std::out_of_range("The module address is not valid under the specified context.");
}

ModuleAddress::~ModuleAddress() { }

uint32_t ModuleAddress::ToImageAddress() const
{
	return ToImageAddress(Context, Value);
}

ModuleAddress::operator uint32_t() const
{
	return Value;
}

uint32_t ModuleAddress::ToImageAddress(const ModuleAddressContext &context, const uint32_t moduleAddress)
{
	if (!IsValidModuleAddress(context, moduleAddress))
		throw std::out_of_range("The module address is not valid under the specified context.");

	return moduleAddress + context.ImageBaseAddress - context.BaseAddress;
}

ModuleAddress ModuleAddress::FromImageAddress(const ModuleAddressContext &context, const uint32_t imageAddress)
{
	uint32_t moduleAddress = imageAddress - context.ImageBaseAddress + context.BaseAddress;

	if (!IsValidModuleAddress(context, moduleAddress))
		throw std::out_of_range("The module address is not valid under the specified context.");

	return ModuleAddress(context, moduleAddress);
}

bool ModuleAddress::IsValidModuleAddress(const ModuleAddressContext &context, const uint32_t moduleAddress)
{
	return moduleAddress >= context.BaseAddress && moduleAddress <= context.BaseAddress + context.Size;
}