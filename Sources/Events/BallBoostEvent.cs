using System;
using Engine;

namespace Pong
{
    /// Fired whenever a ball is getting boosted. Most like due to a collision with a booster.
    public class BallBoostEvent : BroadcastEvent<BallBoostEvent>
    {
        public readonly GameObject ball;

        public BallBoostEvent(GameObject ball)
        {
            this.ball = ball;
        }
    }
}
