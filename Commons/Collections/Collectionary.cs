using System.Collections.Generic;
using System.Linq;
using CG.Commons.Extensions;

namespace CG.Commons.Collections
{
    public class Collectionary<TKey, TValue>
    {
        public Collectionary(bool unique = false)
        {
            _underlyingDictionary = unique ? LazyDictionary.Create<TKey, ICollection<TValue>>(() => new HashSet<TValue>()) :  LazyDictionary.Create<TKey, ICollection<TValue>>(() => new List<TValue>());
        }

        public Collectionary(IEqualityComparer<TValue> listComparer)
        {
            _underlyingDictionary = LazyDictionary.Create<TKey, ICollection<TValue>>(() => new HashSet<TValue>(listComparer));
        }

        private readonly LazyDictionary<TKey, ICollection<TValue>> _underlyingDictionary;

        public void Clear()
        {
            _underlyingDictionary.Clear();
        }

        public void Clear(TKey key)
        {
            _underlyingDictionary[key].Clear();
        }

        public bool Contains(TKey key, TValue item)
        {
            return _underlyingDictionary[key].Contains(item);
        }

        public int KeyCount => _underlyingDictionary.Count;
        public int ValueCount => _underlyingDictionary.Values.Sum(collection => collection.Count);

        public void Add(TKey key, TValue value)
        {
            _underlyingDictionary[key].Add(value);
        }

        public void AddRange(TKey key, IEnumerable<TValue> values)
        {
            _underlyingDictionary[key].AddRange(values);
        }

        public bool ContainsKey(TKey key)
        {
            return _underlyingDictionary.ContainsKey(key);
        }

        public bool HasValues(TKey key)
        {
            return ContainsKey(key) && _underlyingDictionary[key].Any();
        }

        public bool Remove(TKey key)
        {
            return _underlyingDictionary.Remove(key);
        }

        public bool Remove(TKey key, TValue value)
        {
            return _underlyingDictionary.ContainsKey(key) && _underlyingDictionary[key].Remove(value);
        }

        public bool TryGetValue(TKey key, out IEnumerable<TValue> value)
        {
            var result = _underlyingDictionary.TryGetValue(key, out var collection);
            value = collection;
            return result;
        }

        public IEnumerable<TValue> this[TKey key] => _underlyingDictionary[key];
        public ICollection<TKey> Keys => _underlyingDictionary.Keys;
        public IEnumerable<IEnumerable<TValue>> Values => _underlyingDictionary.Values;
        public IEnumerable<TValue> AllValues => _underlyingDictionary.Values.SelectMany(collection => collection);
    }
}
