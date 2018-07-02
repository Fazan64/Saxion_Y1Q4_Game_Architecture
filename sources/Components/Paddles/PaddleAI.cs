using System;
using Engine;
using NUnit.Framework;

namespace Pong
{
    /// An AI-driven controller of a Paddle.
    /// A Paddle must be present on the same GameObject.
    public class PaddleAI : Component
    {
        /// Calculated to produce similar results to the original version.
        const float SpeedModifier = 360f / 125f;

        private Paddle paddle;
        private GameObject ball;

        void Start()
        {
            paddle = Get<Paddle>();
            Assert.IsNotNull(paddle);

            Assert.IsNotNull(ball);

            Vector2 newPosition = gameObject.position;
            newPosition.y = ball.position.y;
            gameObject.position = newPosition;
        }

        void Update()
        {
            Assert.IsNotNull(ball);

            float factor = 1f - Math.Abs(gameObject.position.x - ball.position.x) / game.size.x;
            float deltaY = ball.position.y - gameObject.position.y;
            float yChange = factor * deltaY * SpeedModifier * Game.FixedDeltaTime;

            Vector2 newPosition = gameObject.position;
            newPosition.y += yChange;
            gameObject.position = newPosition;
        }

        public void SetBall(GameObject ball)
        {
            Assert.IsNull(this.ball);
            this.ball = ball;
        }
    }
}
