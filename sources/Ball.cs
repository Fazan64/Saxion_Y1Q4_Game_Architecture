using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spaghetti
{
    class Ball : GameObject
    {
        private bool isBoosting = false;
        private int stunnedCounter = 0;

        // TODO Won't have to make a constructor like this after transitioning to a component-based system.
        public Ball(string name, string file) : base(name, file) {}

        protected override void Start()
        {
            velocity.x = 10;
            Reset();
        }

        public override void Update()
        {
            --stunnedCounter;
            if (stunnedCounter <= 0)
            {
                // Ranging mx to -16..16 using Min/Max

                x += Math.Min(16, Math.Max(-16, isBoosting ? velocity.x * 2.0f : velocity.x)); // limit mx to 2x paddlewith
                y += Math.Max(-16, Math.Min(isBoosting ? velocity.y * 2.0f : velocity.y, 16)); // limit to same

                if (y < 0f)
                {
                    y = 0f;
                    velocity.y *= -1;
                }
                else if (y > 479f - 16f)
                {
                    y = 479f - 16f;
                    velocity.y *= -1;
                }
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
                0.5f * Math.Sign(velocity.x),
                0.15f * (float)(1.0 + Game.random.NextDouble() - 0.5) // random around = or - 0.15f;
            );
            Console.WriteLine(Math.Sign(velocity.x));

            isBoosting = false;
            stunnedCounter = 1000;
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
