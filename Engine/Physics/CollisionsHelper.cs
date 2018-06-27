using System;
using static Engine.Mathf;

namespace Engine.Internal
{
    internal static class CollisionsHelper
    {
        public static bool CheckIntersect(AABB a, AABB b)
        {
            Rect rectA = a.rect;
            rectA.center += a.gameObject.position;

            Rect rectB = b.rect;
            rectB.center += b.gameObject.position;

            return rectA.Overlaps(rectB);
        }

        public static bool CheckIntersect(AABB a, AABB b, out Hit hit)
        {
            hit = new Hit();

            Rect rectA = a.rect;
            rectA.center += a.gameObject.position;

            Rect rectB = b.rect;
            rectB.center += b.gameObject.position;

            Vector2 delta = rectA.center - rectB.center;
            Vector2 penetration = rectA.halfDiagonal + rectB.halfDiagonal - Abs(delta);

            if (penetration.x <= 0f) return false;
            if (penetration.y <= 0f) return false;

            hit.collider = b;

            if (penetration.x < penetration.y)
            {
                float sx = Sign(delta.x);
                hit.delta.x = penetration.x * sx;
                hit.normal.x = sx;
                hit.position.x = rectB.x + sx * rectB.halfDiagonal.x;
                hit.position.y = rectA.y;
            }
            else
            {
                float sy = Sign(delta.y);
                hit.delta.y = penetration.y * sy;
                hit.normal.y = sy;
                hit.position.x = rectA.x;
                hit.position.y = rectB.y + sy * rectB.halfDiagonal.y;
            }

            return true;
        }
    }
}
