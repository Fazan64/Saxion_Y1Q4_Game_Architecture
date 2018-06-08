using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace Spaghetti
{
    class AiPaddle : GameObject
    {
        private Ball ball;

        public AiPaddle(string name, float x, Ball ball) : base(name)
        {
            this.ball = ball;

            position = new Vector2(x - 8, ball.y - 32 + 8);
        }

        void Update()
        {
            // input/events;
            // no input

            // move, track the ball's y;
            float factor = 1f - Math.Abs(x - ball.x) / game.size.x;
            float targetY = ball.y - 32 + 8;
            y += factor * (targetY - y) * Game.FixedDeltaTime;

            // detect hitting wall
            if (y < 0 + 4)
            {
                y = 0 + 4;
            }
            else if (y > 479 - 4 - 64)
            {
                y = 479 - 4 - 64;
            }
        }
    }
}
