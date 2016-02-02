#pragma once

class Modifiable
{
protected:

	// TODO: name member? - likely only to be used for debug logging purposes
	bool _isApplied = false;

public:

	virtual ~Modifiable();

	bool IsApplied() const;
	void Toggle();

	virtual void Apply() = 0;
	virtual void Reset() = 0;
};