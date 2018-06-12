using System;
using System.Drawing;
using Engine;

namespace Spaghetti
{
    public class BallBoostEffectRenderer : Component, IRenderer
    {
        public bool isOn { get; set; }

        public void Render(Graphics graphics)
        {
            if (isOn)
            {
                graphics.DrawEllipse(
                    Pens.White, 
                    gameObject.position.x - 5f, gameObject.position.y - 5f, 
                    26f, 26f
                );
            }
        }
    }
}
