using System;
using Engine;

namespace Engine
{
    public abstract class Collider : Component
    {
        public float bounciness { get; set; } = 1f;

        internal abstract bool HitTest(Collider other, out Hit hit);

        internal abstract bool HitTest(AABB other, out Hit hit);
    }
}
