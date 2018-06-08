using System;
using System.Drawing;

namespace Engine
{
    /// <summary>
    /// Rendering is done through an interface instead of 
    /// picking callbacks using Reflection because renderers might need to 
    /// provide information to the rendering system, such as bounds.
    /// </summary>
    public interface IRenderer
    {
        void Render(Graphics graphics);
    }
}
