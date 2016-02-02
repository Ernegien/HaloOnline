#pragma once
#include <cstdint>
#include <vector>
#include <memory>
#include "Modifiable.hpp"
#include "HookType.hpp"

class Hook : public Modifiable
{
	const uint32_t Address = 0;
	const void* Cave;
	const HookType Type;

	std::vector<uint8_t> OriginalData = { 0 };
	std::vector<uint8_t> ModifiedData = { 0 };

public:

	Hook(const uint32_t address, const void* cave, const HookType type = HookType::Jump);

	void Apply() override;
	void Reset() override;

	static std::shared_ptr<Hook> Create(const uint32_t address, const void* cave, const HookType type = HookType::Jump);
};