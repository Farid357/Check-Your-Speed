using System;
using UnityEngine;

namespace CheckYourSpeed.Model
{
    public interface IPointView : ITransformable
    {
        public void Apply();

        public void Disable();

        public event Action OnDisabled;

    }

    public interface ITransformable
    {
        public Vector3 Position { get; }


        public bool Enable { get; }

    }
}
