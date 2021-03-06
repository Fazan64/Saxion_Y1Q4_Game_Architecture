﻿using System;

namespace Engine
{
    public class Component : EngineObject
    {
        public GameObject gameObject;

        internal Component() {}

        // Shortcuts for inheritors.
        protected T Get<T>()       where T : class     => gameObject.components.Get<T>();
        protected T[] GetAll<T>()  where T : class     => gameObject.components.GetAll<T>();
        protected T Add<T>()       where T : Component => gameObject.components.Add<T>();
        protected bool Has<T>()    where T : class     => gameObject.components.Has<T>();
        protected T GetOrAdd<T>()  where T : Component => gameObject.components.GetOrAdd<T>();
        protected void Remove<T>() where T : class     => gameObject.components.Remove<T>();
    }

}
