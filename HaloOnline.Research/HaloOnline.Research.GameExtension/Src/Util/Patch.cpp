#include "Patch.hpp"

Patch::Patch(const uint32_t address, const std::initializer_list<uint8_t> &modifications) : Address(address), ModifiedData(modifications)
{

}

Patch::Patch(const uint32_t address, const std::vector<uint8_t> modifications) : Address(address), ModifiedData(modifications)
{
	
}

void Patch::Apply()
{
	if (!_isApplied)
	{
		// TODO: apply it
		_isApplied = true;
	}
}

void Patch::Reset()
{
	if (_isApplied)
	{
		// TODO: reset it
		_isApplied = false;
	}
}

std::shared_ptr<Patch> Patch::Create(const uint32_t address, const std::initializer_list<uint8_t> &modifications)
{
	return std::make_shared<Patch>(address, modifications);
}