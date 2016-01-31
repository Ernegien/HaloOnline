#pragma once
#include <unordered_map>

template <typename K, typename V> class DefaultDictionary
{
	const K defaultKey;
	std::unordered_map <const K, V> map;

	public:

		DefaultDictionary(const K &key) : defaultKey(key) { }

		DefaultDictionary(const K &key, std::initializer_list<std::pair<const K, V>> list) : defaultKey(key), map(list) { }

		V& operator[] (const K &key) {
			return map.at(key);
		}

		operator V&()
		{
			return map.at(defaultKey);
		}
};