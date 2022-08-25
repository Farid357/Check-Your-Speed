using System;
using System.Collections.Generic;
using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public  sealed  class  GameObjectsContainerFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        private readonly List<T> _createdObjects = new();
        private readonly IFactory<T> _factory;

        public GameObjectsContainerFactory(IFactory<T> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public IEnumerable<T> CreatedObjects => _createdObjects;

        public T Create()
        {
            var createObject = _factory.Create();
            _createdObjects.Add(createObject);
            return createObject;
        }
    }
}