using System;
using System.Collections;
using System.Collections.Generic;

namespace CG.Commons.Collections
{
    /// <summary>
    /// A simple extension of Dictionary that does not throw any exception when a key is not found.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TValue DefaultValue { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.</summary>
        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param>
        public SafeDictionary(TValue defaultValue = default(TValue))
        {
            DefaultValue = defaultValue;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"></see> and uses the default equality comparer for the key type.</summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"></see> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="dictionary">dictionary</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="dictionary">dictionary</paramref> contains one or more duplicate keys.</exception>
        public SafeDictionary(IDictionary<TKey, TValue> dictionary, TValue defaultValue = default(TValue)) : base(dictionary)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2"></see> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see>.</summary>
        /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2"></see> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2"></see>.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"></see> for the type of the key.</param>
        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="dictionary">dictionary</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="dictionary">dictionary</paramref> contains one or more duplicate keys.</exception>
        public SafeDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer, TValue defaultValue = default(TValue)) : base(dictionary, comparer)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see>.</summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"></see> for the type of the key.</param>
        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param> 
        public SafeDictionary(IEqualityComparer<TKey> comparer, TValue defaultValue = default(TValue)) : base(comparer)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.</summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"></see> can contain.</param>
        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity">capacity</paramref> is less than 0.</exception>
        public SafeDictionary(int capacity, TValue defaultValue = default(TValue)) : base(capacity)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"></see> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see>.</summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2"></see> can contain.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1"></see> for the type of the key.</param>
        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity">capacity</paramref> is less than 0.</exception>
        public SafeDictionary(int capacity, IEqualityComparer<TKey> comparer, TValue defaultValue = default(TValue)) : base(capacity, comparer)
        {
            DefaultValue = defaultValue;
        }

        public new TValue this[TKey key]
        {
            get => ContainsKey(key) ? base[key] : DefaultValue;
            set => base[key] = value;
        }

    }
}
