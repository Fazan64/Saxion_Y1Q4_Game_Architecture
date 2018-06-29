using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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

        public T Add<T>() where T : Component
        {
            Assert.IsFalse(Has<T>(), $"{gameObject} already has {typeof(T)}!");

            T component = Activator.CreateInstance<T>();
            component.gameObject = gameObject;
            Game.main.Add(component);

            list.Add(component);
            OnComponentAdded?.Invoke(gameObject, component);

            return component;
        }

        public T Get<T>() where T : class
        {
            return list
                .Select(c => c as T)
                .Where(c => c != null)
                .FirstOrDefault();
        }

        public T[] GetAll<T>() where T : class
        {
            return list
                .Select(c => c as T)
                .Where(c => c != null)
                .ToArray();
        }

        public void Remove<T>() where T : class
        {
            for (int i = list.Count - 1; i >= 0; --i)
            {
                if (i >= list.Count) continue;

                Component component = list[i];
                if (component is T)
                {
                    list.RemoveAt(i);
                    OnComponentRemoved?.Invoke(gameObject, component);
                }
            }
        }

        public bool Has<T>() where T : class => (Get<T>() != null);

    }

    public static class ComponentsExtensionsGetOrAdd
    {
        public static T GetOrAdd<T>(this Components components) where T : Component
        {
            return components.Get<T>() ?? components.Add<T>();
        }
    }
}
