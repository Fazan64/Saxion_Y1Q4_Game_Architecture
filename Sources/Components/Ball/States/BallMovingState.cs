using System;
using Engine;
using NUnit.Framework;

namespace Pong
{
    public class BallMovingState : FsmState<Ball>,
        IEventReceiver<PointScoreEvent>,
        IEventReceiver<BallBoostEvent>
    {
        private const float InitialHorizontalSpeed  = 360f * 0.5f;
        private const float MaxInitialVerticalSpeed = 360f;

        private bool nextTimeStartTowardsRight;
        private Random random;

        public override void Enter()
        {
            base.Enter();

            agent.rigidbody.velocity = GetNewVelocity();
        }

        public void On(BallBoostEvent eventData)
        {
            if (!isEntered) return;
            if (eventData.ball != gameObject) return;

            agent.fsm.ChangeState<BallBoostedState>();
        }

        public void On(PointScoreEvent eventData)
        {
            // Make it go towards the loser.
            nextTimeStartTowardsRight = eventData.leftPlayerScored;
        }

        private Vector2 GetNewVelocity()
        {
            float direction = nextTimeStartTowardsRight ? 1f : -1f;

            random = random ?? Services.Get<Random>();
            Assert.IsNotNull(random);
            float verticalSpeedModifier = (float)(random.NextDouble() + 0.5) * 0.15f;

            return new Vector2(
                InitialHorizontalSpeed * direction,
                MaxInitialVerticalSpeed * verticalSpeedModifier
            );
        }
    }
}
