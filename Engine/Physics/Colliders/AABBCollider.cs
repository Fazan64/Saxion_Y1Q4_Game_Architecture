﻿using System;
using Engine.Internal;

namespace Engine
{
    // An axis-aligned bounding box
    public class AABBCollider : Collider
    {
        /// The rect is in local coordinates, i.e. with origin being 
        /// at the position of this GameObject.
        public Rect rect;

        internal override bool HitTest(Collider other, out Hit hit)
        {
            return other.HitTest(this, out hit);
        }

        internal override bool HitTest(AABBCollider other, out Hit hit)
        {
            return CollisionDetectionHelper.CheckIntersect(
                other.GetWorldspaceRect(), this.GetWorldspaceRect(), 
                out hit
            );
        }

        public Rect GetWorldspaceRect()
        {
            Rect worldspaceRect = rect;
            worldspaceRect.min += gameObject.position;
            return worldspaceRect;
        }
    }
}
