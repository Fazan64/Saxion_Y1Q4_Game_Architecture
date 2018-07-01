using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;
using NUnit.Framework;

namespace Pong
{
    public class Ball : Component, 
        IEventReceiver<PointScoreEvent>,
        IEventReceiver<BallCollidedWithBoosterEvent>
    {
        private const float MaxSpeed = 300f;
        private const float InitialHorizontalSpeed = 200f;
        private const float MaxInitialVerticalSpeed = 70f;

        private bool isBoosting = false;
        private float stunnedCounter = 0f;

        private Rigidbody rb;
        private BallBoostEffectRenderer boostEffectRenderer;

        void Start()
        {
            Console.WriteLine(this + ": Start");

            rb = Get<Rigidbody>();
            Assert.IsNotNull(rb);
            rb.velocity.x = 10f;

            boostEffectRenderer = GetOrAdd<BallBoostEffectRenderer>();
            boostEffectRenderer.isOn = isBoosting;

            Reset();

            new TestEvent("Some test data").Post();
        }

        void Update()
        {
            if (stunnedCounter > 0f) 
            {
                stunnedCounter -= Game.FixedDeltaTime;
                if (stunnedCounter > 0f) return;
            }

            Vector2 extraVelocity = isBoosting ? rb.velocity : Vector2.zero;
            extraVelocity = extraVelocity.TruncatedBy(MaxSpeed / 2f);
            gameObject.position += extraVelocity * Game.FixedDeltaTime;

            // TODO do this in a framerate-based update, not the fixed-timestep one.
            boostEffectRenderer.isOn = isBoosting;
        }

        void OnCollision(Collision collision)
        {
            if (collision.gameObject.Has<Paddle>())
            {
                isBoosting = false;
            }
        }

        public void On(PointScoreEvent pointScore)
        {
            Reset();
        }

        public void On(BallCollidedWithBoosterEvent eventData)
        {
            if (eventData.ball != gameObject) return;

            isBoosting = true;
        }

        private void Reset()
        {
            gameObject.position = new Vector2(320f - 5f, 240f - 5f);

            rb.velocity = new Vector2(
                InitialHorizontalSpeed * Math.Sign(rb.velocity.x),
                MaxInitialVerticalSpeed * (float)(Services.Get<Random>().NextDouble() - 0.5) * 2f
            );

            isBoosting = false;
            stunnedCounter = 1f;
        }
    }
}
