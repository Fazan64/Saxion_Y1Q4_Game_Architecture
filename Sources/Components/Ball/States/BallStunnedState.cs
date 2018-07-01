using System;
using Engine;

namespace Pong
{
    public class BallStunnedState : FsmState<Ball>
    {
        private float stunnedCounter = 0f;

        public override void Enter()
        {
            base.Enter();

            stunnedCounter = 1f;
        }

        void Update()
        {
            if (!isEntered) return;

            stunnedCounter -= Game.FixedDeltaTime;
            if (stunnedCounter <= 0f) 
            {
                agent.fsm.ChangeState<BallMovingState>();
            }
        }
    }
}
