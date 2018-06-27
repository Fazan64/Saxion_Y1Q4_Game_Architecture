using System;
using System.Drawing;
using NUnit.Framework;

namespace Engine
{
    public class ImageRenderer : Component, IRenderer
    {
        public Image image { get; set; }
        public Vector2 pivot { get; set; } = Vector2.half;

        public void Render(Graphics graphics)
        {
            if (image == null) return;

            Assert.IsFalse(Vector2.IsNaN(gameObject.position));

            Vector2 offset = new Vector2(
                -pivot.x * image.Size.Width,
                -pivot.y * image.Size.Height
            );
            Vector2 position = gameObject.position + offset; 

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
