using System;
using Engine;

namespace Spaghetti
{
    public class BallEnteredBoostZoneEvent : EngineEvent<BallEnteredBoostZoneEvent>
    {
        public readonly GameObject ball;

        public BallEnteredBoostZoneEvent(GameObject ball)
        {
            this.ball = ball;
        }
    }
}
