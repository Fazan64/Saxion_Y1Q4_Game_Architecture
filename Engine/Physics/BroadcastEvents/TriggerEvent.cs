using System;

namespace Engine
{
    /// This event is fired when a collider hits a trigger collider. 
    public class TriggerEvent : BroadcastEvent<TriggerEvent>
    {
        public readonly Collider colliderA;
        public readonly Collider colliderB;

        public TriggerEvent(Collider colliderA, Collider colliderB)
        {
            this.colliderA = colliderA;
            this.colliderB = colliderB;
        }
    }
}
