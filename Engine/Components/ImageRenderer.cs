using System;
using System.Drawing;
using NUnit.Framework;

namespace Engine
{
    public class ImageRenderer : Component, IRenderer
    {
        public Image image { get; set; }
        public Vector2 pivot { get; set; }

        public void Render(Graphics graphics)
        {
            if (image == null) return;

            Vector2 position = gameObject.position - new Vector2(pivot.x * image.Size.Width, pivot.y * image.Size.Height);

            graphics.DrawImage(
                image, 
                position.x, 
                position.y
            );
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
