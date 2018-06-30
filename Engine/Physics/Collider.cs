using System;
using Engine;

namespace Engine
{
    public abstract class Collider : Component
    {
        public float bounciness { get; set; } = 1f;

        /// If true, collisions with this collider will not be resolved, i.e. 
        /// objects will pass through it. However, a TriggerEvent will be broadcast
        /// and the OnTrigger method will be called on all behaviours involved.
        public bool isTrigger { get; set; }

        internal abstract bool HitTest(Collider other, out Hit hit);

        internal abstract bool HitTest(AABBCollider other, out Hit hit);
    }
}
