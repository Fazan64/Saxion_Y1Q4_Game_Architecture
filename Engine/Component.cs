using System;

namespace Engine
{
    public class Component : EngineObject, IBehaviour
    {
        public GameObject gameObject;
        internal readonly Callbacks callbacks;

        internal Component()
        {
            callbacks = new Callbacks(this);
        }

        Callbacks IBehaviour.GetCallbacks() => callbacks;

        // Shortcuts for inheritors.
        protected T Get<T>() where T : class => gameObject.components.Get<T>();
        protected T Add<T>() where T : Component => gameObject.components.Add<T>();
        protected bool Has<T>() where T : class => gameObject.components.Has<T>();
        protected T GetOrAdd<T>() where T : Component => gameObject.components.GetOrAdd<T>();
        protected void Remove<T>() where T : class => gameObject.components.Remove<T>();
    }

}
