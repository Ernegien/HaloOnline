#include "ProcessAddress.h"

bool ProcessAddress::IsInitialized;
uint32_t ProcessAddress::ImageBaseAddress;
uint32_t ProcessAddress::ProcessBaseAddress;

ProcessAddress::ProcessAddress(const uint32_t &address)
{
	Value = address;
}

ProcessAddress::~ProcessAddress()
{

}

void ProcessAddress::Initialize(const uint32_t &imageBase, const uint32_t &processBase)
{
	ImageBaseAddress = imageBase;
	ProcessBaseAddress = processBase;
	IsInitialized = true;
}

uint32_t ProcessAddress::ToImageAddress(const uint32_t &processAddress)
{
	if (!IsInitialized)
	{
		throw std::exception("ProcessAddress::Initialize must be called first.");
	}
	
	return processAddress + ImageBaseAddress - ProcessBaseAddress;
}

uint32_t ProcessAddress::ToImageAddress() const
{
	if (!IsInitialized)
	{
		throw std::exception("ProcessAddress::Initialize must be called first.");
	}

	return Value + ImageBaseAddress - ProcessBaseAddress;
}

uint32_t ProcessAddress::FromImageAddress(const uint32_t &imageAddress)
{
	if (!IsInitialized)
	{
		throw std::exception("ProcessAddress::Initialize must be called first.");
	}

	return imageAddress - ImageBaseAddress + ProcessBaseAddress;
}

ProcessAddress::operator uint32_t() const
{
	return Value;
}
