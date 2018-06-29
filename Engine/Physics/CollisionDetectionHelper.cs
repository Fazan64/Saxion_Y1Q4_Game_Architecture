using System;
using static Engine.Mathf;

namespace Engine.Internal
{
    internal static class CollisionDetectionHelper
    {
        /// Checks Rect a against Rect b. 
        /// `a` is assumed to be moving against `b`, so
        /// hit.delta will be the displacement by which `a` needs to be moved
        /// to stop penetration.
        /// hit.normal will be from `b` to `a`.
        public static bool CheckIntersect(Rect a, Rect b, out Hit hit)
        {
            hit = new Hit();

            Vector2 delta = a.center - b.center;
            Vector2 penetration = a.halfDiagonal + b.halfDiagonal - Abs(delta);

            if (penetration.x <= 0f) return false;
            if (penetration.y <= 0f) return false;

            if (penetration.x < penetration.y)
            {
                float sx = Sign(delta.x);
                hit.delta.x = penetration.x * sx;
                hit.normal.x = sx;
                hit.position.x = b.center.x + sx * b.halfDiagonal.x;
                hit.position.y = a.center.y;
            }
            else
            {
                float sy = Sign(delta.y);
                hit.delta.y = penetration.y * sy;
                hit.normal.y = sy;
                hit.position.x = a.center.x;
                hit.position.y = b.center.y + sy * b.halfDiagonal.y;
            }

            return true;
        }
    }
}
