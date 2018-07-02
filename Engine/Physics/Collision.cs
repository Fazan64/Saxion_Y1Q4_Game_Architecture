using System;

namespace Engine
{
    public struct Collision
    {
        public readonly GameObject gameObject;
        public readonly Collider   collider;
        public readonly Rigidbody  rigidbody;

        public Collision(GameObject gameObject, Collider collider, Rigidbody rigidbody = null)
        {
            this.gameObject = gameObject;
            this.collider   = collider;
            this.rigidbody  = rigidbody;
        }
    }
}
