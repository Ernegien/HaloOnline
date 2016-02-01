#include "Patch.hpp"

Patch::Patch(const uint32_t address, PatchBytes &modifications) : Address(address), ModifiedData(modifications)
{

}

//Patch::Patch(const uint32_t address, std::vector<uint8_t> modifications) : IModification(address, modifications)
//{
//	
//}

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

std::shared_ptr<Patch> Patch::Create(const uint32_t address, PatchBytes &modifications)
{
	return std::make_shared<Patch>(address, modifications);
}