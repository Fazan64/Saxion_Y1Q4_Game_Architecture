using System;
using Engine;
using NUnit.Framework;

namespace Spaghetti
{
    public class Booster : Component
    {
        public GameObject ball { get; set; }

        void Start()
        {
            Assert.IsNotNull(ball);
        }

        void Update()
        {
            if (
                ball.x      < gameObject.position.x + 16 && 
                ball.x + 16 > gameObject.position.x - 16 && 
                ball.y      < gameObject.position.y + 16 && 
                ball.y + 16 > gameObject.position.y - 16
            )
            {
                new BallCollidedWithBooster(ball).Post();
            }
        }
    }
}
