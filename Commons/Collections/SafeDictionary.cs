using System;
using System.Collections;
using System.Collections.Generic;

namespace CG.Commons.Collections
{
    /// <summary>
    /// A simple IDictionary implementation that does not throw any exception when a key is not found.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _data;

        public TValue DefaultValue { get; }

        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param>
        public SafeDictionary(TValue defaultValue = default(TValue))
        {
            DefaultValue = defaultValue;
            _data = new Dictionary<TKey, TValue>();
        }

        /// <param name="dictionary"></param>
        /// <param name="defaultValue">The default value that will be returned when a key is not found.</param>
        public SafeDictionary(Dictionary<TKey, TValue> dictionary, TValue defaultValue = default(TValue))
        {
            DefaultValue = defaultValue;
            _data = dictionary;
        }

        public TValue this[TKey key]
        {
            get => ContainsKey(key) ? _data[key] : DefaultValue;
            set => _data[key] = value;
        }

        #region Implementation

        public void Add(TKey key, TValue value)
        {
            _data.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _data.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return _data.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _data.TryGetValue(key, out value);
        }

        public ICollection<TKey> Keys => _data.Keys;
        public ICollection<TValue> Values => _data.Values;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _data.Clear();
        }

        #region KeyValuePair Methods

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        public int Count => _data.Count;
        public bool IsReadOnly => false;

        #endregion
    }
}
