#include "ModificationSet.hpp"
#include "Modifiable.hpp"
#include <memory>

ModificationSet::ModificationSet(const std::initializer_list<std::shared_ptr<Modifiable>> &modifications) : Modifications(modifications) { }

void ModificationSet::Apply()
{
	if (!_isApplied)
	{
		for (auto const& mod : Modifications) {
			mod->Apply();
		}
		_isApplied = true;
	}
}

void ModificationSet::Reset()
{
	if (_isApplied)
	{
		for (auto const& mod : Modifications) {
			mod->Reset();
		}
		_isApplied = false;
	}
}

std::shared_ptr<ModificationSet> ModificationSet::Create(const std::initializer_list<std::shared_ptr<Modifiable>> &modifications)
{
	return std::make_shared<ModificationSet>(modifications);
}