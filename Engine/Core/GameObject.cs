using System;
using Engine.Internal;

namespace Engine
{
    public class GameObject : EngineObject
    {
        public string name { get; set; }
        public Vector2 position;

        public readonly Components components;

        internal readonly GameObjectPhysics physics;

        public GameObject(string name = "GameObject")
        {
            components = new Components(this);
            physics = new GameObjectPhysics(this);

            this.name = name;

            Game.main.Add(this);
        }

        public override string ToString()
        {
            return $"GameObject {name}";
        }
    }

    public static class GameObjectExtensions 
    {
        // Shortcuts
        public static T Get<T>      (this GameObject go) where T : class     => go.components.Get<T>();
        public static T[] GetAll<T> (this GameObject go) where T : class     => go.components.GetAll<T>();
        public static T Add<T>      (this GameObject go) where T : Component => go.components.Add<T>();
        public static bool Has<T>   (this GameObject go) where T : class     => go.components.Has<T>();
        public static T GetOrAdd<T> (this GameObject go) where T : Component => go.components.GetOrAdd<T>();
        public static void Remove<T>(this GameObject go) where T : class     => go.components.Remove<T>();
    }
}
