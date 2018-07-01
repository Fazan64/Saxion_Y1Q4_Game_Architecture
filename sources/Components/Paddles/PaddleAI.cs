using System;
using Engine;
using NUnit.Framework;

namespace Pong
{
    /// An AI-driven controller of a Paddle.
    /// A Paddle must be present on the same GameObject.
    public class PaddleAI : Component
    {
        private Paddle paddle;
        private GameObject ball;

        void Start()
        {
            paddle = Get<Paddle>();
            Assert.IsNotNull(paddle);

            Assert.IsNotNull(ball);
            gameObject.position.y = ball.position.y;
        }

        void Update()
        {
            Assert.IsNotNull(ball);

            float factor = 1f - Math.Abs(gameObject.position.x - ball.position.x) / game.size.x;
            float deltaY = ball.position.y - gameObject.position.y;
            float yChange = factor * deltaY * Game.FixedDeltaTime * (100f / 60f);

            gameObject.position.y += yChange;
        }

        public void SetBall(GameObject ball)
        {
            Assert.IsNull(this.ball);
            this.ball = ball;
        }
    }
}
