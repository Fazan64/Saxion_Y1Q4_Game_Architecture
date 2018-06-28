using System;

namespace Engine
{
    public class CollisionEvent : BroadcastEvent<CollisionEvent>
    {
        public readonly GameObject gameObjectA;
        public readonly Collider   colliderA  ;
        public readonly Rigidbody  rigidbodyA ;

        public readonly GameObject gameObjectB;
        public readonly Collider   colliderB  ;
        public readonly Rigidbody  rigidbodyB ;

        public CollisionEvent(
            GameObject gameObjectA, Collider colliderA, Rigidbody rigidbodyA,
            GameObject gameObjectB, Collider colliderB, Rigidbody rigidbodyB
        )
        {
            this.gameObjectA = gameObjectA;
            this.colliderA   = colliderA;
            this.rigidbodyA  = rigidbodyA;

            this.gameObjectB = gameObjectB;
            this.colliderB   = colliderB;
            this.rigidbodyB  = rigidbodyB;
        }
    }
}
