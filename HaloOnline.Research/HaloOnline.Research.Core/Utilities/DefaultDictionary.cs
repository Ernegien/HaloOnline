using System;
using System.Collections.Generic;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// TODO: description
    /// </summary>
    public class DefaultDictionary<TKey,TValue> : Dictionary<TKey,TValue>
    {
        /// <summary>
        /// TODO: description
        /// </summary>
        public TKey DefaultKey { get; }

        /// <summary>
        /// TODO: descriptione
        /// </summary>
        /// <param name="defaultKey"></param>
        public DefaultDictionary(TKey defaultKey)
        {
            if (defaultKey == null)
                throw new ArgumentNullException();
            
            DefaultKey = defaultKey;
        }

        /// <summary>
        /// TODO: description
        /// </summary>
        /// <param name="dictionary"></param>
        public static implicit operator TValue(DefaultDictionary<TKey, TValue> dictionary)
        {
            return dictionary[dictionary.DefaultKey];
        }
    }
}
