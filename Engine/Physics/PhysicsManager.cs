using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Engine.Internal
{
    /// Steps the physics simulation.
    /// Calls OnCollision and OnTrigger methods on all registered EngineObject|s
    internal class PhysicsManager
    {
        private readonly HashSet<EngineObject> registered = new HashSet<EngineObject>();

        private readonly List<GameObjectPhysics> gameObjectsPhysics;
        private readonly List<Collider> colliders;

        public PhysicsManager()
        {
            gameObjectsPhysics = new List<GameObjectPhysics>();
            colliders = new List<Collider>();

            CollisionEvent.OnDeliver += On;
            TriggerEvent.OnDeliver   += On;
        }

        public void Add(EngineObject engineObject)
        {
            Assert.IsFalse(registered.Contains(engineObject));

            if (engineObject is GameObject gameObject)
            {
                gameObjectsPhysics.Add(gameObject.physics);
            }
            else if (engineObject is Collider collider)
            {
                colliders.Add(collider);
            }
            else return;

            registered.Add(engineObject);
        }

        public void Remove(EngineObject engineObject)
        {
            Assert.IsTrue(registered.Contains(engineObject));

            if (engineObject is GameObject gameObject)
            {
                bool didRemove = gameObjectsPhysics.Remove(gameObject.physics);
                Assert.IsTrue(didRemove);
            }
            else if (engineObject is Collider collider)
            {
                bool didRemove = colliders.Remove(collider);
                Assert.IsTrue(didRemove);
            }
            else return;

            registered.Remove(engineObject);
        }

        /// Steps the physics simulation one fixed timestep forward
        public void Step()
        {
            foreach (GameObjectPhysics physics in gameObjectsPhysics)
            {
                if (physics.rigidbody == null) continue;

                physics.rigidbody.Step(colliders);
            }
        }

        private static void On(CollisionEvent collisionEvent)
        {
            collisionEvent.gameObjectA.physics.onCollision?.Invoke(MakeCollisionForA(collisionEvent));
            collisionEvent.gameObjectB.physics.onCollision?.Invoke(MakeCollisionForB(collisionEvent));
        }

        private static void On(TriggerEvent triggerEvent)
        {
            triggerEvent.colliderA.gameObject.physics.onTrigger?.Invoke(triggerEvent.colliderB);
            triggerEvent.colliderB.gameObject.physics.onTrigger?.Invoke(triggerEvent.colliderA);
        }

        private static Collision MakeCollisionForA(CollisionEvent c)
        {
            return new Collision(
                c.gameObjectB, c.colliderB, c.rigidbodyB
            );
        }

        private static Collision MakeCollisionForB(CollisionEvent c)
        {
            return new Collision(
                c.gameObjectA, c.colliderA, c.rigidbodyA
            );
        }
    }
}
