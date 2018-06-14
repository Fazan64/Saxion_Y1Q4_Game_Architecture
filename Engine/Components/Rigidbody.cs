using System;

namespace Engine
{
    public class Rigidbody : Component
    {
        public Vector2 velocity;

        // TODO Start with having bounds directly on the rb. 
        // Might extract a Collider class later.
        // Resolve collision and post a Collision event when bounds overlap.

        void Update()
        {
            gameObject.position += velocity * Game.FixedDeltaTime;
        }
    }
}
