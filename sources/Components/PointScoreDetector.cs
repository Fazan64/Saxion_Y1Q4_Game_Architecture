using System;
using Engine;

namespace Spaghetti
{
    public class PointScoreDetector : Component
    {
        public bool isRightPlayer { get; set; }

        void OnCollision(Collision collision)
        {
            if (!collision.gameObject.Has<Ball>()) return;

            if (isRightPlayer)
            {
                PointScoreEvent.leftScored.Post();
            }
            else 
            {
                PointScoreEvent.rightScored.Post();
            }
        }
    }
}
