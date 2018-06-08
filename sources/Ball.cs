using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace Spaghetti
{
    class Ball : GameObject
    {
        private const float MaxSpeed = 300f;
        private const float InitialHorizontalSpeed = 200f;
        private const float MaxInitialVerticalSpeed = 70f;

        private bool isBoosting = false;
        private float stunnedCounter = 0f;

        // TODO Won't have to make a constructor like this after transitioning to a component-based system.
        public Ball(string name, string fileName) : base(name, fileName) {}

        protected override void Start()
        {
            velocity.x = 10f;
            Reset();
        }

        public override void Update()
        {
            if (stunnedCounter > 0f) 
            {
                stunnedCounter -= Game.FixedDeltaTime;
                if (stunnedCounter > 0f) return;
            }

            Vector2 finalVelocity = isBoosting ? velocity : velocity * 2f;
            finalVelocity = finalVelocity.TruncatedBy(MaxSpeed);
            position += finalVelocity * Game.FixedDeltaTime;

            if (y < 0f)
            {
                y = 0f;
                velocity.y *= -1f;
            }
            else if (y > 479f - 16f)
            {
                y = 479f - 16f;
                velocity.y *= -1f;
            }
        }

        public override void Render(Graphics graphics)
        {
            base.Render(graphics);
            if (isBoosting)
            {
                graphics.DrawEllipse(Pens.White, position.x - 5f, position.y - 5f, 26f, 26f);
            }
        }

        public void Reset()
        {
            position = new Vector2(320f - 5f, 240f - 5f);

            velocity = new Vector2(
                InitialHorizontalSpeed * Math.Sign(velocity.x),
                MaxInitialVerticalSpeed * (float)(Game.random.NextDouble() - 0.5) * 2f // random around = or - 0.15f;
            );
            Console.WriteLine(Math.Sign(velocity.x));

            isBoosting = false;
            stunnedCounter = 1f;
        }

        public void Resolve(float x, float y, float mx, float my)
        {
            position = new Vector2(x, y);
            velocity = new Vector2(mx, my);
            isBoosting = false;
        }

        public void SetBoost(bool value)
        {
            isBoosting = value;
        }
    }
}
