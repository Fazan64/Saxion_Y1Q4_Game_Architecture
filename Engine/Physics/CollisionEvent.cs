using System;

namespace Engine
{
    public class CollisionEvent : BroadcastEvent<CollisionEvent>
    {
        public readonly GameObject collideeA;
        public readonly GameObject collideeB;

        public CollisionEvent(GameObject collideeA, GameObject collideeB)
        {
            this.collideeA = collideeA;
            this.collideeB = collideeB;
        }
    }
}
