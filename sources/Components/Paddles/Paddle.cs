using System;
using Engine;
using NUnit.Framework;

namespace Spaghetti
{
    public class Paddle : Component
    {
        public float maxSpeedY { get; set; } = 100f;

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

            float minY = 0f + 4f;
            float maxY = game.size.y - 1f - 4f - 64f;
            gameObject.position.y = Mathf.Clamp(gameObject.position.y, minY, maxY);

            didRequestMovementSinceLastUpdate = false;
        }

        void OnCollision(Collision collision)
        {
            Ball ball = collision.gameObject.Get<Ball>();
            if (ball == null) return;

            //float dy = Math.Abs((gameObject.position.y - 32) - (collision.gameObject.position.y + 8)) / 50.0f;

            //Assert.IsNotNull(collision.rigidbody);
            //collision.rigidbody.velocity.y *=
        }

        public void SetMoveTarget(float targetY)
        {
            this.targetY = targetY;
            didRequestMovementSinceLastUpdate = true;
        }
    }
}
