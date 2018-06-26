using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

#pragma warning disable RECS0020 // Delegate subtraction has unpredictable result.
// Problems only happen when subtracting a sequence of delegates in a multicast delegate.
// Only single delegates are being subtracted here.

namespace Engine
{
    /// Efficiently keeps track of physics-related components on a gameobject. 
    public class GameObjectPhysics
    {
        public event Action<GameObjectPhysics> OnUpdate;

        private readonly List<Collider> _colliders = new List<Collider>();

        public Rigidbody rigidbody { get; private set; }
        public IEnumerable<Collider> colliders => _colliders.AsReadOnly();
        public Action<Collision> onCollision { get; private set; }

        public GameObjectPhysics(GameObject gameObject)
        {
            onCollision = gameObject.GetDelegate<Action<Collision>>("OnCollision");

            AddExistingComponentsOf(gameObject);

            gameObject.components.OnComponentAdded   += OnComponentAdded;
            gameObject.components.OnComponentRemoved += OnComponentRemoved;
        }

        private void AddExistingComponentsOf(GameObject gameObject)
        {
            rigidbody = rigidbody ?? gameObject.Get<Rigidbody>();
            _colliders.AddRange(gameObject.GetAll<Collider>());

            foreach (var behaviour in gameObject.GetAll<IBehaviour>())
            {
                onCollision += behaviour.GetCallbacks().onCollision;
            }
        }

        private void OnComponentAdded(GameObject gameObject, Component component)
        {
            bool didUpdate = false;

            if (component is IBehaviour behaviour)
            {
                onCollision += behaviour.GetCallbacks().onCollision;

                didUpdate = true;
            }

            if (component is Rigidbody newRigidbody)
            {
                Assert.IsNull(rigidbody);
                rigidbody = newRigidbody;

                didUpdate = true;
            }
            else if (component is Collider collider)
            {
                Assert.IsFalse(_colliders.Contains(collider));
                _colliders.Add(collider);

                didUpdate = true;
            }

            if (didUpdate)
            {
                OnUpdate?.Invoke(this);
            }
        }

        private void OnComponentRemoved(GameObject gameObject, Component component)
        {
            bool didUpdate = false;

            if (component is IBehaviour behaviour)
            {
                onCollision -= behaviour.GetCallbacks().onCollision;

                didUpdate = true;
            }

            if (component is Rigidbody)
            {
                Assert.IsNotNull(rigidbody);
                rigidbody = null;

                didUpdate = true;
            }
            else if (component is Collider collider)
            {
                Assert.IsTrue(_colliders.Contains(collider));
                _colliders.Remove(collider);

                didUpdate = true;
            }

            if (didUpdate)
            {
                OnUpdate?.Invoke(this);
            }
        }
    }
}
