using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Engine
{
    public class PhysicsManager
    {
        private readonly HashSet<EngineObject> registered = new HashSet<EngineObject>();

        private readonly HashSet<GameObjectPhysics> gameObjectsPhysics;
        private readonly HashSet<Collider> colliders;

        public PhysicsManager()
        {
            gameObjectsPhysics = new HashSet<GameObjectPhysics>();
            colliders = new HashSet<Collider>();

            CollisionEvent.OnDeliver += On;
        }

        public void Add(EngineObject engineObject)
        {
            Assert.IsFalse(registered.Contains(engineObject));

            if (engineObject is GameObject gameObject)
            {
                bool didAdd = gameObjectsPhysics.Add(gameObject.physics);
                Assert.IsTrue(didAdd);
            }
            else if (engineObject is Collider collider)
            {
                bool didAdd = colliders.Add(collider);
                Assert.IsTrue(didAdd);
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
            collisionEvent.collideeA.physics.onCollision?.Invoke(MakeCollisionForA(collisionEvent));
            collisionEvent.collideeB.physics.onCollision?.Invoke(MakeCollisionForB(collisionEvent));
        }

        private static Collision MakeCollisionForA(CollisionEvent collisionEvent)
        {
            return new Collision(collisionEvent.collideeB);
        }

        private static Collision MakeCollisionForB(CollisionEvent collisionEvent)
        {
            return new Collision(collisionEvent.collideeA);
        }
    }
}
