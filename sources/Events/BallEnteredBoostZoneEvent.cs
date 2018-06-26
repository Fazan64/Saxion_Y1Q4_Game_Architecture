using System;
using Engine;

namespace Spaghetti
{
    public class BallEnteredBoostZoneEvent : BroadcastEvent<BallEnteredBoostZoneEvent>
    {
        public readonly GameObject ball;

        public BallEnteredBoostZoneEvent(GameObject ball)
        {
            this.ball = ball;
        }
    }
}
