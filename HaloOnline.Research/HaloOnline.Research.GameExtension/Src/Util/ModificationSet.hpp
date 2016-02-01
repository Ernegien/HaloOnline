#pragma once
#include "IModification.hpp"

class ModificationSet : public IModification
{
	const std::vector<std::shared_ptr<IModification>> Modifications;

public:
	ModificationSet(const std::initializer_list<std::shared_ptr<IModification>> &modifications) : Modifications(modifications) { }
	//ModificationSet(const std::initializer_list<ModificationSet> &modifications)
	//{
	//	// TODO:
	//}

	void Apply() override
	{
		if (!_isApplied)
		{
			// TODO: loop through each mod and apply it
			_isApplied = true;
		}
	}
	
	void Reset() override
	{
		if (_isApplied)
		{
			// TODO: reset it
			_isApplied = false;
		}
	}
};