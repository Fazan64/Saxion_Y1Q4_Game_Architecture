using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace Spaghetti
{
    class Ball : Component
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

            if (gameObject.y < 0f)
            {
                gameObject.y = 0f;
                rb.velocity.y *= -1f;
            }
            else if (gameObject.y > 479f - 16f)
            {
                gameObject.y = 479f - 16f;
                rb.velocity.y *= -1f;
            }

            // TODO do this in a framerate-based update, not the fixed-timestep one.
            boostEffectRenderer.isOn = isBoosting;
        }

        public void Reset()
        {
            gameObject.position = new Vector2(320f - 5f, 240f - 5f);

            rb.velocity = new Vector2(
                InitialHorizontalSpeed * Math.Sign(rb.velocity.x),
                MaxInitialVerticalSpeed * (float)(Game.random.NextDouble() - 0.5) * 2f
            );
            Console.WriteLine(Math.Sign(rb.velocity.x));

            isBoosting = false;
            stunnedCounter = 1f;
        }

        public void Resolve(float x, float y, float mx, float my)
        {
            gameObject.position = new Vector2(x, y);
            rb.velocity         = new Vector2(mx, my);
            isBoosting          = false;
        }

        public void SetBoost(bool value)
        {
            isBoosting = value;
        }
    }
}
