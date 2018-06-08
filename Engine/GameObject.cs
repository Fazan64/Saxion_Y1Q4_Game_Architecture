using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Engine
{
    public class GameObject : EngineObject
    {
        public string name { get; set; }
        public Image image { get; set; }
        public Vector2 position;
        public Vector2 velocity;

        public readonly Components components;
        internal readonly Callbacks callbacks;

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
            callbacks  = new Callbacks(this);
            components = new Components(this);

            this.name = name;
            this.image = Image.FromFile(imageFileName);
            Debug.Assert(image != null);
            Start();
        }

        // TODO Get a version of the update manager from the modified gxp engine. 
        protected virtual void Start() {}
        public virtual void Update() 
        {
            position += velocity * Game.FixedDeltaTime;
        }

        public virtual void Render(Graphics graphics)
        {
            graphics.DrawImage(image, position.x, position.y);
        }
    }
}
