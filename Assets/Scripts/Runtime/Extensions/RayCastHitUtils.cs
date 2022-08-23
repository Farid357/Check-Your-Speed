using UnityEngine;

namespace CheckYourSpeed.Utils
{
    public static class RayCastHitUtils
    {
        public static bool Hit<T>(this RaycastHit2D raycastHit, out T t) where T : class
        {
            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.TryGetComponent<T>(out var component))
                {
                    t = component;
                    return true;
                }

                t = null;
                return false;
            }
            t = null;
            return false;
        }
    }
}