using System;

namespace Engine
{
    public abstract class Collider : Component
    {
        public abstract bool CheckIntersect(Collider other);

        public abstract bool CheckIntersect(AABB other);
    }
}
