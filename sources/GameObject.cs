using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Spaghetti
{
    public class GameObject
    {
        public string name { get; set; }
        public Image image { get; set; }
        public Vector2 position;
        public Vector2 velocity;

        public float x
        {
            get { return position.x; }
            set { position = new Vector2(value, y); }
        }
        public float y
        {
            get { return position.y; }
            set { position = new Vector2(x, value); }
        }

        public GameObject(string name, string imageFileName)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(name));
            Debug.Assert(!string.IsNullOrWhiteSpace(imageFileName));

            this.name = name;
            this.image = Image.FromFile(imageFileName);
            Start();
        }

        // TODO Get a version of the update manager from the modified gxp engine. 
        protected virtual void Start() {}
        public virtual void Update() {}

        public virtual void Render(Graphics graphics)
        {
            graphics.DrawImage(image, position.x, position.y);
        }
    }
}
