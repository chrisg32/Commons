using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Collections
{
    /// <summary>
    /// Work in progress
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class LazyDictionary<TKey, TValue> : IDictionary<TKey, TValue> //,ISerializable ,IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDeserializationCallback
    {
        public static LazyDictionary<TKeyType, TValueType> Create<TKeyType, TValueType>() where TValueType : new()
        {
            return new LazyDictionary<TKeyType, TValueType>(() => new TValueType());
        }

        public static LazyDictionary<TKeyType, TValueType> Create<TKeyType, TValueType>(Func<TValueType> initializer)
        {
            return new LazyDictionary<TKeyType, TValueType>(initializer);
        }

        public static LazyDictionary<TKeyType, TValueType> Create<TKeyType, TValueType>(Func<TKeyType, TValueType> initializer)
        {
            return new LazyDictionary<TKeyType, TValueType>(initializer);
        }

        private readonly Func<TKey, TValue> _initializer;
        private readonly Dictionary<TKey, TValue> _underlyingDictionary = new Dictionary<TKey, TValue>();

        private LazyDictionary(Func<TValue> initializer)
        {
            _initializer = key => initializer();
        }

        private LazyDictionary(Func<TKey, TValue> initializer)
        {
            _initializer = initializer;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _underlyingDictionary.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _underlyingDictionary.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _underlyingDictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _underlyingDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _underlyingDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _underlyingDictionary.Remove(item.Key);
        }

        public int Count => _underlyingDictionary.Count;
        public bool IsReadOnly => false;
        public bool ContainsKey(TKey key)
        {
            return _underlyingDictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            _underlyingDictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return _underlyingDictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _underlyingDictionary.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!_underlyingDictionary.ContainsKey(key))
                {
                    _underlyingDictionary[key] = _initializer(key);
                }
                return _underlyingDictionary[key];
            }
            set { _underlyingDictionary[key] = value; }
        }

        public ICollection<TKey> Keys => _underlyingDictionary.Keys;
        public ICollection<TValue> Values => _underlyingDictionary.Values;
        
    }
}
