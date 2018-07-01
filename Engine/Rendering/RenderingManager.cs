using System;
using System.Drawing;
using System.Collections.Generic;
using NUnit.Framework;

namespace Engine
{
    /// Renders all added EngineObject|s that implement IRenderer
    internal class RenderingManager
    {
        private readonly HashSet<IRenderer> registered = new HashSet<IRenderer>();

        public void Render(Graphics graphics)
        {
            foreach (IRenderer renderer in registered)
            {
                renderer.Render(graphics);
            }
        }

        public void Add(EngineObject engineObject)
        {
            if (engineObject is IRenderer renderer)
            {
                Assert.IsFalse(registered.Contains(renderer));

                registered.Add(renderer);
            }
        }

        public void Remove(EngineObject engineObject)
        {
            if (engineObject is IRenderer renderer)
            {
                Assert.IsTrue(registered.Contains(renderer));

                registered.Remove(renderer);
            }
        }
    }
}
