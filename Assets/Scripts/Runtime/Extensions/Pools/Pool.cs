using System;
using System.Collections.Generic;

namespace CheckYourSpeed.Utils
{
    public sealed class Pool<T> : IPool<T>
    {
        private readonly IFactory<T> _factory;
        private readonly Stack<T> _objects = new();

        public Pool(IFactory<T> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        private bool IsEmpty => _objects.Count == 0;

        public T Get()
        {
            return IsEmpty ? _factory.Create() : _objects.Pop();
        }

        public void Release(T obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            _objects.Push(obj);
        }
    }
}