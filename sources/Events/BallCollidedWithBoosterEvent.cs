using System;
using Engine;

namespace Pong
{
    public class BallCollidedWithBoosterEvent : BroadcastEvent<BallCollidedWithBoosterEvent>
    {
        public readonly GameObject ball;

        public BallCollidedWithBoosterEvent(GameObject ball)
        {
            this.ball = ball;
        }
    }
}
