using System;
using Engine;

namespace Spaghetti
{
    public class BallCollidedWithBooster : BroadcastEvent<BallCollidedWithBooster>
    {
        public readonly GameObject ball;

        public BallCollidedWithBooster(GameObject ball)
        {
            this.ball = ball;
        }
    }
}
