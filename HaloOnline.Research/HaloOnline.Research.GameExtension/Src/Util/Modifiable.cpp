#include "Modifiable.hpp"

Modifiable::~Modifiable() { };

bool Modifiable::IsApplied() const
{
	return _isApplied;
}

void Modifiable::Toggle()
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