using System;
using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public sealed class UnityDictionary<TKey, TValue> : MonoBehaviour
    {
        [SerializeField] private List<TKey> _keys;
        [SerializeField] private List<TValue> _value;

        public TValue this[int index]
        {
            get => Value[index];
            set => _value[index] = value;
        }

        public int Count => _keys.Count;

        public IReadOnlyList<TValue> Value => _value;

        public IReadOnlyList<TKey> Keys => _keys;

        private void Start() => CheckNullFields();

        private void OnValidate() => TuneDictionary(_value);

        public void Add(TKey key, TValue value)
        {
            _keys.Add(key);
            _value.Add(value);
        }

        public void Clear()
        {
            _keys = null;
            _value = null;
        }

        public void Clear(int index)
        {
            if (index > _keys.Count)
                throw new ArgumentOutOfRangeException();

            _keys.RemoveAt(index);
            _value.RemoveAt(index);
        }

        public void Clear(TKey key)
        {
            _value.RemoveAt(_keys.IndexOf(key));
            _keys.Remove(key);
        }

        public void Clear(TValue value)
        {
            _keys.RemoveAt(_value.IndexOf(value));
            _value.Remove(value);
        }

        public void Clear(int index, int count)
        {
            _keys.RemoveRange(index, count);
            _value.RemoveRange(index, count);
        }

        public bool ContainsValue(TValue value) => _value.Contains(value);

        public bool ContainsKey(TKey value) => _keys.Contains(value);

        private void CheckNullFields()
        {
            foreach (var value in _value)
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