using System;
using Engine.Internal;

namespace Engine
{
    // An axis-aligned bounding box
    public class AABB : Collider
    {
        public Rect rect;

        internal override bool HitTest(Collider other, out Hit hit)
        {
            return other.HitTest(this, out hit);
        }

        internal override bool HitTest(AABB other, out Hit hit)
        {
            return CollisionDetectionHelper.CheckIntersect(other, this, out hit);
        }
    }
}
