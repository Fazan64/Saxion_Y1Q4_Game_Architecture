using System;
using System.Collections.Generic;

namespace Engine
{
    public class Rigidbody : Component
    {
        public Vector2 velocity;

        internal void Step(IEnumerable<Collider> colliders)
        {
            gameObject.position += velocity * Game.FixedDeltaTime;

            foreach (Collider ownCollider in gameObject.physics.colliders)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider == ownCollider) continue;

                    if (ownCollider.CheckIntersect(collider))
                    {
                        new CollisionEvent(gameObject, collider.gameObject).Post();
                    }
                }
            }
        }

        void OnCollision(Collision collision)
        {
            Console.WriteLine(collision);
        }
    }
}
