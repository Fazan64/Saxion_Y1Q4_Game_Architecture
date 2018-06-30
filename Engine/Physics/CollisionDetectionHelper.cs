using System;
using static Engine.Mathf;

namespace Engine.Internal
{
    internal static class CollisionDetectionHelper
    {
        /// Checks if a Rect intersects another Rect.
        /// If so, genetates an appropriate Hit with detailed info about the intersection.
        /// The first Rect (`mover`) is assumed to be moving relative to the other one (`stator`).
        /// hit.delta will be the displacement by which `mover` needs to be moved
        /// to stop penetration.
        /// hit.normal will be away from `stator`.
        public static bool CheckIntersect(Rect mover, Rect stator, out Hit hit)
        {
            hit = new Hit();

            Vector2 delta = mover.center - stator.center;
            Vector2 penetration = mover.halfDiagonal + stator.halfDiagonal - Abs(delta);

            if (penetration.x <= 0f) return false;
            if (penetration.y <= 0f) return false;

            hit.position = mover.center;

            if (penetration.x <= penetration.y)
            {
                float sx = Sign(delta.x);
                hit.delta.x = penetration.x * sx;
                hit.normal.x = sx;
                hit.position.x = stator.center.x + sx * stator.halfDiagonal.x;
            }

            if (penetration.x >= penetration.y)
            {
                float sy = Sign(delta.y);
                hit.delta.y = penetration.y * sy;
                hit.normal.y = sy;
                hit.position.y = stator.center.y + sy * stator.halfDiagonal.y;
            }

            return true;
        }
    }
}
