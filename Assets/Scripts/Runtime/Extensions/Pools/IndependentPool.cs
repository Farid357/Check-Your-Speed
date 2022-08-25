using System;
using System.Collections.Generic;
using CheckYourSpeed.Model;
using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public  sealed  class  IndependentPool<T> : IUpdateble, IPool<T> where T : MonoBehaviour
    {
        private readonly IPool<T> _pool;
        private readonly GameObjectsContainerFactory<T> _factory;
        private readonly List<T> _releasedObjects = new();
        
        public IndependentPool(GameObjectsFactory<T> gameObjectsFactory)
        {
            if (gameObjectsFactory is null)
                throw new ArgumentNullException(nameof(gameObjectsFactory));
            
            _factory = new GameObjectsContainerFactory<T>(gameObjectsFactory);
            _pool = new Pool<T>(_factory);
        }

        private IEnumerable<T> CreatedObjects => _factory.CreatedObjects;
        
        public void Release(T obj) => _pool.Release(obj);

        public T Get() => _pool.Get();

        public void Update(float deltaTime)
        {
            foreach (var item in CreatedObjects)
            {
                if (item.gameObject.activeInHierarchy == false && _releasedObjects.Contains(item) == false)
                {
                    _releasedObjects.Add(item);
                    Release(item);
                }
                else
                {
                    if(_releasedObjects.Contains(item))
                    _releasedObjects.Remove(item);
                }
            }
        }
    }
}