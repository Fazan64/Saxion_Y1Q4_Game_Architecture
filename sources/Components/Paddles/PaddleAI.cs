using System;
using Engine;
using NUnit.Framework;

namespace Spaghetti
{
    public class PaddleAI : Component
    {
        private Paddle paddle;
        private GameObject ball;

        void Start()
        {
            paddle = Get<Paddle>();
            Assert.IsNotNull(paddle);

            Assert.IsNotNull(ball);
            //gameObject.position.y = ball.y;
        }

        void Update()
        {
            Assert.IsNotNull(ball);
            paddle.SetMoveTarget(ball.y);
            //gameObject.position.y = ball.y;
        }

        public void SetBall(GameObject ball)
        {
            Assert.IsNull(this.ball);
            this.ball = ball;
        }
    }
}
