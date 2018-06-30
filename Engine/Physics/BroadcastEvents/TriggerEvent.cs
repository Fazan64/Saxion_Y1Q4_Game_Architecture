using System;

namespace Engine
{
    /// This event is broadcast whenever there is a collision between
    /// two colliders, at least one of which is a trigger.
    /// NOTE: At least one of the collidees needs to have a Rigidbody.
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
