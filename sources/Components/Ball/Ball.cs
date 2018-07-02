using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;
using NUnit.Framework;

namespace Pong
{
    public class Ball : Component, 
        IEventReceiver<PointScoreEvent>
    {
        private const float MaxSpeed = 300f;

        public bool isBoosting { get; set; }

        public Rigidbody rigidbody { get; private set; }
        private BallBoostEffectRenderer boostEffectRenderer;

        public FiniteStateMachine<Ball> fsm { get; private set; }

        void Start()
        {
            Add<BallMovingState>();
            fsm = new FiniteStateMachine<Ball>(this);

            rigidbody = Get<Rigidbody>();
            Assert.IsNotNull(rigidbody);

            boostEffectRenderer = GetOrAdd<BallBoostEffectRenderer>();

            Reset();
        }

        void Update()
        {
            Vector2 newVelocity = rigidbody.velocity;
            newVelocity.x = Mathf.Clamp(newVelocity.x, -MaxSpeed, +MaxSpeed);
            newVelocity.y = Mathf.Clamp(newVelocity.y, -MaxSpeed, +MaxSpeed);
            rigidbody.velocity = newVelocity;

            if (isBoosting)
            {
                gameObject.position += rigidbody.velocity * Game.FixedDeltaTime;
            }

            boostEffectRenderer.isOn = isBoosting;
        }

        public void On(PointScoreEvent pointScore)
        {
            Reset();
        }

        private void Reset()
        {
            gameObject.position = game.size * 0.5f;
            rigidbody.velocity = Vector2.zero;

            isBoosting = false;

            fsm.ChangeState<BallStunnedState>();
        }
    }
}
