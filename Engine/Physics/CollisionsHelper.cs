using System;

namespace Engine
{
    public static class CollisionsHelper
    {
        public static bool CheckIntersect(AABB a, AABB b)
        {
            Rect rectA = a.rect;
            rectA.center += a.gameObject.position;

            Rect rectB = b.rect;
            rectB.center += b.gameObject.position;

            return rectA.Overlaps(rectB);
        }
    }
}
