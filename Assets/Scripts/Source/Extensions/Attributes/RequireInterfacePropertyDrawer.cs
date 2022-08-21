using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CheckYourSpeed.Utils
{
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public sealed class RequireInterfacePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool isGameObjectCollection = typeof(IEnumerable<MonoBehaviour>).IsAssignableFrom(fieldInfo.FieldType);
            bool isValid = fieldInfo.FieldType == typeof(MonoBehaviour) || isGameObjectCollection;

            if (isValid == false)
            {
                EditorGUI.HelpBox(position, "It's not monobehaviour or monobehaviours collection!", MessageType.Error);
                return;
            }

            var needAttribute = attribute as RequireInterfaceAttribute;

            CheckDragAndDrops(position, needAttribute.Type);
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue,
    typeof(MonoBehaviour), needAttribute.AllowSceneObjects);

            TryClearValues(property, needAttribute.Type);
        }

        private void CheckDragAndDrops(Rect position, Type type)
        {
            if (position.Contains(Event.current.mousePosition))
            {
                var count = DragAndDrop.objectReferences.Length;

                for (int i = 0; i < count; i++)
                {
                    if (InvalidObject(DragAndDrop.objectReferences[i], type))
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                        break;
                    }
                }
            }
        }

        private bool InvalidObject(UnityEngine.Object obj, Type type)
        {
            var go = obj as MonoBehaviour;
            if (go != null)
                return go.GetComponent(type) == null;

            return true;
        }

        private void TryClearValues(SerializedProperty property, Type type)
        {
            if (property.objectReferenceValue != null)
            {
                if (InvalidObject(property.objectReferenceValue, type))
                {
                    property.objectReferenceValue = null;
                }
            }
        }
    }
}