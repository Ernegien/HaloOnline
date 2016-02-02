#pragma once
#include <memory>
#include <vector>
#include "Modifiable.hpp"

class Patch : public Modifiable
{
	const uint32_t Address = 0;

	// TODO: patch flags

	std::vector<uint8_t> OriginalData = { 0 };
	std::vector<uint8_t> ModifiedData = { 0 };

public:

	// TODO: allow for the ability to specify original data as an argument, might want persistence via source code and the ability to detect modification state
	// TODO: add patch flags as an argument - PauseThread | PreserveProtect etc.
	Patch(const uint32_t address, const std::initializer_list<uint8_t> &modifications);
	Patch(const uint32_t address, const std::vector<uint8_t> modifications);

	void Apply() override;
	void Reset() override;

	static std::shared_ptr<Patch> Create(const uint32_t address, const std::initializer_list<uint8_t> &modifications);
};
