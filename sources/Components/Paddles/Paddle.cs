using System;
using Engine;
using NUnit.Framework;

namespace Spaghetti
{
    public class Paddle : Component
    {
        public float maxSpeedY { get; set; } = 50f;

        private bool didRequestMovementSinceLastUpdate;
        private float targetY;

        void Update()
        {
            if (!didRequestMovementSinceLastUpdate) return;

            float maxDelta = maxSpeedY * Game.FixedDeltaTime;
            gameObject.position.y = Mathf.MoveTowards(
                gameObject.position.y, targetY, 
                maxDelta
            );

            float minY = 0f + 4f + 32f;
            float maxY = game.size.y - 1f - 4f - 32f;
            gameObject.position.y = Mathf.Clamp(gameObject.position.y, minY, maxY);

            didRequestMovementSinceLastUpdate = false;
        }

        void OnCollision(Collision collision)
        {
            Ball ball = collision.gameObject.Get<Ball>();
            if (ball == null) return;

            float deltaY = Mathf.Abs(gameObject.position.y - collision.gameObject.position.y);
            float normalizedDeltaY = deltaY / (64f * 0.5f);
            Console.WriteLine("Hit " + normalizedDeltaY); // just for testing

            float yMultiplier = 1f + normalizedDeltaY * 2f;

            Assert.IsNotNull(collision.rigidbody);
            collision.rigidbody.velocity.y *= yMultiplier;
        }

        public void SetMoveTarget(float targetY)
        {
            this.targetY = targetY;
            didRequestMovementSinceLastUpdate = true;
        }
    }
}
