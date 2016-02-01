#include "IModification.hpp"

IModification::~IModification() { };

bool IModification::IsApplied() const
{
	return _isApplied;
}

void IModification::Toggle()
{
	if (!_isApplied)
	{
		Apply();
	}
	else
	{
		Reset();
	}
}