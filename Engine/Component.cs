using System;

namespace Engine
{
    public class Component : EngineObject, IBehaviour
    {
        public GameObject gameObject;

        internal Component()
        {
            Game.main.Add(this);
        }

        // Shortcuts for inheritors.
        protected T Get<T>()       where T : class     => gameObject.components.Get<T>();
        protected T Add<T>()       where T : Component => gameObject.components.Add<T>();
        protected bool Has<T>()    where T : class     => gameObject.components.Has<T>();
        protected T GetOrAdd<T>()  where T : Component => gameObject.components.GetOrAdd<T>();
        protected void Remove<T>() where T : class     => gameObject.components.Remove<T>();
    }

}
