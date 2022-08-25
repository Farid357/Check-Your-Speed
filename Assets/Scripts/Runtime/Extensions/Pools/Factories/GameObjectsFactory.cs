using System;
using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public sealed class GameObjectsFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;

        public GameObjectsFactory(T prefab, Transform parent = null)
        {
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
            _parent = parent;
        }

        public T Create()
        {
            var createObject = UnityEngine.Object.Instantiate(_prefab, _parent);
            return createObject;
        }
    }
}