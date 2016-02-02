#pragma once
#include "Hook.hpp"
#include <memory>

Hook::Hook(const uint32_t address, const void* cave, const HookType type) : Address(address), Cave(cave), Type(type)
{

}

void Hook::Apply()
{
	if (!_isApplied)
	{
		// TODO: apply it
		_isApplied = true;
	}
}

void Hook::Reset()
{
	if (_isApplied)
	{
		// TODO: reset it
		_isApplied = false;
	}
}

std::shared_ptr<Hook> Hook::Create(const uint32_t address, const void* cave, const HookType type)
{
	return std::make_shared<Hook>(address, cave, type);
}