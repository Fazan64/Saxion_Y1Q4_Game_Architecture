using System;
using Engine;

namespace Pong
{
    public class BallBoostedState : FsmState<Ball>
    {
        public override void Enter()
        {
            base.Enter();

            agent.rigidbody.velocity *= 2f;
            agent.boostEffectRenderer.isOn = true;
        }

        void OnCollision(Collision collision)
        {
            if (!isEntered) return;

            if (collision.gameObject.Has<Paddle>())
            {
                agent.fsm.ChangeState<BallMovingState>();
            }
        }

        public override void Exit()
        {
            base.Exit();

            agent.rigidbody.velocity *= 0.5f;
            agent.boostEffectRenderer.isOn = false;
        }
    }
}
