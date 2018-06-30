using System;

namespace Engine
{
    /// What's returned by Collider.CheckIntersect;
    public struct Hit
    {
        public Vector2 normal;
        public Vector2 delta;
        public Vector2 position;

        public override string ToString()
        {
            return $"[Hit: normal: {normal}, delta: {delta}, position: {position}]";
        }
    }
}
