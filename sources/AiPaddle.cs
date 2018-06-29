using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace Penne
{
    class AiPaddle : GameObject
    {
        private GameObject ball;

        public AiPaddle(string name, float x, GameObject ball) : base(name)
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
            float targetY = ball.y - 32f + 8f;
            y += factor * (targetY - y) * Game.FixedDeltaTime;

            float minY = 0f + 4f;
            float maxY = game.size.y - 1f - 4f - 64f;
            y = Mathf.Clamp(y, minY, maxY);
        }
    }
}
