using System;
using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.Utils
{
    [Serializable]
    public abstract class UnityDictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey> _keys;
        [SerializeField] private List<TValue> _values;

        public TValue this[int index]
        {
            get => Values[index];
            set => _values[index] = value;
        }

        public int Count => _keys.Count;

        public IReadOnlyList<TValue> Values => _values;

        public IReadOnlyList<TKey> Keys => _keys;

        private void Start() => CheckNullFields();

        private void OnValidate() => TuneDictionary(_values);

        public void Add(TKey key, TValue value)
        {
            _keys.Add(key);
            _values.Add(value);
        }

        public void Clear()
        {
            _keys = null;
            _values = null;
        }

        public void Clear(int index)
        {
            if (index > _keys.Count)
                throw new ArgumentOutOfRangeException();

            _keys.RemoveAt(index);
            _values.RemoveAt(index);
        }

        public void Clear(TKey key)
        {
            _values.RemoveAt(_keys.IndexOf(key));
            _keys.Remove(key);
        }

        public void Clear(TValue value)
        {
            _keys.RemoveAt(_values.IndexOf(value));
            _values.Remove(value);
        }

        public void Clear(int index, int count)
        {
            _keys.RemoveRange(index, count);
            _values.RemoveRange(index, count);
        }

        public bool ContainsValue(TValue value) => _values.Contains(value);

        public bool ContainsKey(TKey value) => _keys.Contains(value);

        private void CheckNullFields()
        {
            foreach (var value in _values)
            {
                if (value == null)
                    Debug.LogError("Value is null!");
            }
        }

        private void TuneDictionary(List<TValue> value)
        {
            if (value == null || value.Count == Keys.Count)
                return;

            if (value.Count > Keys.Count)
                value.RemoveAt(Keys.Count);
            else
                value.Add(default);
        }
    }
}