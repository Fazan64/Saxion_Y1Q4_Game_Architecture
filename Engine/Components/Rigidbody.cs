using System;

namespace Engine
{
    public class Rigidbody : Component
    {
        public Vector2 velocity;

        void Update()
        {
            gameObject.position += velocity * Game.FixedDeltaTime;
        }
    }
}
