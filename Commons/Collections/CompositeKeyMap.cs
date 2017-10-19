using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Commons.Collections
{
    public class CompositeKeyMap<TKey, TValue> : BaseCompositeKeyMap<TKey, TValue>
    {
        public new TValue this[TKey key1]
        {
            get => base[key1];
            set => base[key1] = value;
        }

        public new void Add(TKey key1, TValue value)
        {
            base.Add(key1, value);
        }

        public new bool ContainsKey(TKey key1)
        {
            return base.ContainsKey(key1);
        }

        public new bool Remove(TKey key1)
        {
            return base.Remove(key1);
        }
    }

    public class CompositeKeyMap<TKey1, TKey2, TValue> : BaseCompositeKeyMap<Tuple<TKey1, TKey2>, TValue>
    {
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get => this[Tuple.Create(key1, key2)];
            set => this[Tuple.Create(key1, key2)] = value;
        }

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            Add(Tuple.Create(key1, key2), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2)
        {
            return ContainsKey(Tuple.Create(key1, key2));
        }

        public bool Remove(TKey1 key1, TKey2 key2)
        {
            return Remove(Tuple.Create(key1, key2));
        }
    }

    public class CompositeKeyMap<TKey1, TKey2, TKey3, TValue> : BaseCompositeKeyMap<Tuple<TKey1, TKey2, TKey3>, TValue>
    {
        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3]
        {
            get => this[Tuple.Create(key1, key2, key3)];
            set => this[Tuple.Create(key1, key2, key3)] = value;
        }

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TValue value)
        {
            Add(Tuple.Create(key1, key2, key3), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3)
        {
            return ContainsKey(Tuple.Create(key1, key2, key3));
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3)
        {
            return Remove(Tuple.Create(key1, key2, key3));
        }
    }

    public class CompositeKeyMap<TKey1, TKey2, TKey3, TKey4, TValue> : BaseCompositeKeyMap<Tuple<TKey1, TKey2, TKey3, TKey4>, TValue>
    {
        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4]
        {
            get => this[Tuple.Create(key1, key2, key3, key4)];
            set => this[Tuple.Create(key1, key2, key3, key4)] = value;
        }

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TValue value)
        {
            Add(Tuple.Create(key1, key2, key3, key4), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
        {
            return ContainsKey(Tuple.Create(key1, key2, key3, key4));
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4)
        {
            return Remove(Tuple.Create(key1, key2, key3, key4));
        }
    }

    public class CompositeKeyMap<TKey1, TKey2, TKey3, TKey4, TKey5, TValue> : BaseCompositeKeyMap<Tuple<TKey1, TKey2, TKey3, TKey4, TKey5>, TValue>
    {
        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5]
        {
            get => this[Tuple.Create(key1, key2, key3, key4, key5)];
            set => this[Tuple.Create(key1, key2, key3, key4, key5)] = value;
        }

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TValue value)
        {
            Add(Tuple.Create(key1, key2, key3, key4, key5), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5)
        {
            return ContainsKey(Tuple.Create(key1, key2, key3, key4, key5));
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5)
        {
            return Remove(Tuple.Create(key1, key2, key3, key4, key5));
        }
    }

    public class CompositeKeyMap<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TValue> : BaseCompositeKeyMap<Tuple<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>, TValue>
    {
        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6]
        {
            get => this[Tuple.Create(key1, key2, key3, key4, key5, key6)];
            set => this[Tuple.Create(key1, key2, key3, key4, key5, key6)] = value;
        }

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TValue value)
        {
            Add(Tuple.Create(key1, key2, key3, key4, key5, key6), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6)
        {
            return ContainsKey(Tuple.Create(key1, key2, key3, key4, key5, key6));
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6)
        {
            return Remove(Tuple.Create(key1, key2, key3, key4, key5, key6));
        }
    }

    public class CompositeKeyMap<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TValue> : BaseCompositeKeyMap<Tuple<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>, TValue>
    {
        public TValue this[TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7]
        {
            get => this[Tuple.Create(key1, key2, key3, key4, key5, key6, key7)];
            set => this[Tuple.Create(key1, key2, key3, key4, key5, key6, key7)] = value;
        }

        public void Add(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TValue value)
        {
            Add(Tuple.Create(key1, key2, key3, key4, key5, key6, key7), value);
        }

        public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7)
        {
            return ContainsKey(Tuple.Create(key1, key2, key3, key4, key5, key6, key7));
        }

        public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7)
        {
            return Remove(Tuple.Create(key1, key2, key3, key4, key5, key6, key7));
        }
    }

    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    [System.Runtime.InteropServices.ComVisible(false)]
    public abstract class BaseCompositeKeyMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly Dictionary<TKey, TValue> _map = new Dictionary<TKey, TValue>();

        protected TValue this[TKey key]
        {
            get => _map[key];
            set => _map[key] = value;
        }

        protected void Add(TKey key, TValue value)
        {
            _map.Add(key, value);
        }

        public void Clear()
        {
            _map.Clear();
        }

        protected bool ContainsKey(TKey key)
        {
            return _map.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return _map.ContainsValue(value);
        }

        protected bool Remove(TKey key)
        {
            return _map.Remove(key);
        }

        public int Count => _map.Count;

        public IEnumerator GetEnumerator()
        {
            return _map.GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _map.GetEnumerator();
        }
    }
}