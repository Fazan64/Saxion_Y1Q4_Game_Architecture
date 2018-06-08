using System;
using System.Drawing;
using NUnit.Framework;

namespace Engine
{
    public class ImageRenderer : Component, IRenderer
    {
        public Image image { get; set; }

        public void Render(Graphics graphics)
        {
            if (image == null) return;
            graphics.DrawImage(image, gameObject.position.x, gameObject.position.y);
        }

        public void SetImage(Image image)
        {
            this.image = image;
        }

        public void SetImage(string imageFilePath)
        {
            image = Image.FromFile(imageFilePath);
            Assert.IsNotNull(image);
        }
    }
}
