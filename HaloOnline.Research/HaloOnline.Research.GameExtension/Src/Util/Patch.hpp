#pragma once
#include "IModification.hpp"
#include <memory>
#include <vector>

typedef const std::initializer_list<uint8_t> PatchBytes;

class Patch : public IModification
{
	uint32_t Address = 0;

	// TODO: patch flags

	std::vector<uint8_t> OriginalData = { 0 };
	std::vector<uint8_t> ModifiedData = { 0 };

public:

	// TODO: add patch flags as an argument - PauseThread | PreserveProtect etc.
	explicit Patch(const uint32_t address, PatchBytes &modifications);
	//explicit Patch(const uint32_t address, std::vector<uint8_t> modifications);

	void Apply() override;
	void Reset() override;

	static std::shared_ptr<Patch> Create(const uint32_t address, PatchBytes &modifications);
};
