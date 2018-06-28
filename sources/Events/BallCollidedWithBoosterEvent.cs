using System;
using Engine;

namespace Spaghetti
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
