#pragma once

class IModification
{
protected:
	bool _isApplied = false;

public:

	virtual ~IModification();

	bool IsApplied() const;
	void Toggle();

	virtual void Apply() = 0;
	virtual void Reset() = 0;
};