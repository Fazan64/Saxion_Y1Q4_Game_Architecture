using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Engine
{
    public class GameObject : EngineObject
    {
        public string name { get; set; }
        public Vector2 position;

        public readonly Components components;

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

        public GameObject(string name)
        {
            components = new Components(this);

            this.name = name;

            Game.main.Add(this);
        }

        // Shortcuts
        public T Get<T>()       where T : class     => components.Get<T>();
        public T Add<T>()       where T : Component => components.Add<T>();
        public bool Has<T>()    where T : class     => components.Has<T>();
        public T GetOrAdd<T>()  where T : Component => components.GetOrAdd<T>();
        public void Remove<T>() where T : class     => components.Remove<T>();
    }
}
