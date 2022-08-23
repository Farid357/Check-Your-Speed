using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public static class MonoBehaviourUtils
    {
        ///Only for monobehaviours which contains RequireInterfaceAttribute
        public static T ToInterface<T>(this MonoBehaviour monoBehaviour) where T : class
        {
            return monoBehaviour as T;
        }
    }
}
