using System;
using System.Drawing;
using Engine;

namespace Pong
{
    /// Draws a custom `boost` effect around its gameobject.
    public class BallBoostEffectRenderer : Component, IRenderer
    {
        const float Width  = 26f;
        const float Height = 26f;

        public bool isOn { get; set; }

        public void Render(Graphics graphics)
        {
            if (!isOn) return;

            graphics.DrawEllipse(
                Pens.White, 
                gameObject.position.x - Width * 0.5f, gameObject.position.y - Height * 0.5f, 
                Width, Height
            );
        }
    }
}
