using System;
using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public sealed class RequireInterfaceAttribute : PropertyAttribute
    {
        public readonly Type Type;
        public readonly bool AllowSceneObjects;

        public RequireInterfaceAttribute(Type type, bool allowSceneObjects = true)
        {
            Type = type;
            AllowSceneObjects = allowSceneObjects;
        }
    }
}