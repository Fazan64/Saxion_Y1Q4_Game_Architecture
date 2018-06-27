using System;

namespace Engine
{
    public class Collision
    {
        public readonly GameObject gameObject;
        public readonly Collider collider;
        public readonly Rigidbody rigidbody;

        public Collision(GameObject collidee, Collider collider, Rigidbody rigidbody = null)
        {
            this.gameObject = collidee;
            this.collider   = collider;
            this.rigidbody  = rigidbody;
        }
    }
}
