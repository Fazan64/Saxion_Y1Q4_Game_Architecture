using System;

namespace Engine
{
    public class AABB : Collider
    {
        public Rect rect;

        public override bool CheckIntersect(Collider other)
        {
            return other.CheckIntersect(this);
        }

        public override bool CheckIntersect(AABB other)
        {
            return CollisionsHelper.CheckIntersect(other, this);
        }
    }
}
