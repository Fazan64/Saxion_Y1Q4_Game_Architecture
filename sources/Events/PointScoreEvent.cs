using System;
using Engine;

namespace Spaghetti
{
    public class PointScoreEvent : EngineEvent<PointScoreEvent>
    {
        public static PointScoreEvent leftScored => new PointScoreEvent(false);
        public static PointScoreEvent rightScored => new PointScoreEvent(true);

        private readonly bool isRight;

        public bool rightPlayerScored => isRight;
        public bool leftPlayerScored => !isRight;

        private PointScoreEvent(bool isRight)
        {
            this.isRight = isRight;
        }
    }
}
