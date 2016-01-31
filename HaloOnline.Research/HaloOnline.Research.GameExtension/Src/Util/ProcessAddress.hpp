#pragma once
#include <stdint.h>

class ProcessAddress
{
	static const char* InitializationErrorMessage;
	static bool IsInitialized;
	static uint32_t ImageBaseAddress;
	static uint32_t ProcessBaseAddress;
	uint32_t Value;

	public:

		static void Initialize(const uint32_t &imageBase, const uint32_t &processBase);
		static uint32_t ToImageAddress(const uint32_t &processAddress);
		static uint32_t FromImageAddress(const uint32_t &imageAddress);

		ProcessAddress(const uint32_t &processAddress);
		~ProcessAddress();

		uint32_t ToImageAddress() const;

		operator uint32_t() const;
};

