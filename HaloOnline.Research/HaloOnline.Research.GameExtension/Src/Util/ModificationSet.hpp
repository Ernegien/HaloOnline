#pragma once
#include "Modifiable.hpp"
#include <memory>
#include <vector>

class ModificationSet : public Modifiable
{
	std::vector<std::shared_ptr<Modifiable>> Modifications;

public:

	ModificationSet(const std::initializer_list<std::shared_ptr<Modifiable>> &modifications);

	void Apply() override;
	void Reset() override;

	static std::shared_ptr<ModificationSet> Create(const std::initializer_list<std::shared_ptr<Modifiable>> &modifications);
};
