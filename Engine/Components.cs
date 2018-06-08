using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine
{
    /// Stores and retrieves the components of a GameObject
    public class Components : IEnumerable<Component>
    {
        public delegate void ComponentEventHandler(GameObject gameObject, Component component);
        public event ComponentEventHandler OnComponentAdded;
        public event ComponentEventHandler OnComponentRemoved;

        private readonly GameObject gameObject;
        private readonly List<Component> list = new List<Component>();

        public Components(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        // IEnumerable implementation
        public IEnumerator<Component> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        public Component[] GetAll() => list.ToArray();

        public bool Has<T>() where T : class => (Get<T>() != null);

        public T Add<T>() where T : Component
        {
            if (Has<T>())
            {
                throw new InvalidOperationException($"{gameObject} already has {typeof(T)}!");
            }

            var component = Activator.CreateInstance<T>();
            component.gameObject = gameObject;

            list.Add(component);

            OnComponentAdded?.Invoke(gameObject, component);

            return component;
        }

        public T Get<T>() where T : class
        {
            var result = list.Find(component => component is T);
            return result as T;
        }

        public void Remove<T>() where T : class
        {
            for (int i = 0; i < list.Count; ++i)
            {
                var component = list[i];
                if (component is T)
                {
                    list.RemoveAt(i);
                    OnComponentRemoved?.Invoke(gameObject, component);
                }
            }
        }
    }

    public static class ComponentsExtensionsGetOrAdd
    {
        public static T GetOrAdd<T>(this Components components) where T : Component
        {
            return components.Get<T>() ?? components.Add<T>();
        }
    }
}
