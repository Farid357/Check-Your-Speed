using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CheckYourSpeed.Utils
{
    public sealed class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly List<T> _objects = new();
        private readonly int _count;

        public ObjectPool(int count, T prefab, Transform parent = null)
        {
            _count = count > 0 ? count : throw new System.ArgumentOutOfRangeException(nameof(count));
            Add(_count, prefab, parent);
        }

        public void Add(int count, T prefab, Transform parent = null)
        {
            for (int i = 0; i < count; i++)
            {
                var createObject = Object.Instantiate(prefab, parent);
                createObject.gameObject.SetActive(false);
                _objects.Add(createObject);
            }
        }

        private T GetDisable<T1>(T prefab)
        {
            return _objects.FirstOrDefault(o => !o.gameObject.activeInHierarchy && o.GetType() == prefab.GetType());
        }

        public T Get(T prefab)
        {
            if (HaveObject(_objects))
            {
                return GetDisable<T>(prefab);
            }

            else
            {
                Add(_count, prefab);
                return GetDisable<T>(prefab);
            }
        }

        private bool HaveObject(List<T> objects)
        {
            return objects.Any(o => o.gameObject.activeInHierarchy == false);
        }
    }
}