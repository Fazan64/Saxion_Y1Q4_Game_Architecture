﻿using System;
using System.Collections.Generic;
using Engine.Internal;
using NUnit.Framework;

namespace Engine
{
    /// This component is responsible for physics-based movement
    /// and collision detection and resolution.
    /// Whenever a collision is detected, broadcasts a CollisionEvent or a TriggerEvent.
    public class Rigidbody : Component
    {
        public Vector2 velocity;
        public float inverseMass = 1f;

        public float mass
        {
            get => 1f / inverseMass;
            set => inverseMass = 1f / value;
        }

        internal void Step(IEnumerable<Collider> colliders)
        {
            Assert.IsFalse(Vector2.IsNaN(gameObject.position));
            Assert.IsFalse(Vector2.IsNaN(velocity));

            gameObject.position += velocity * Game.FixedDeltaTime;

            foreach (Collider ownCollider in gameObject.physics.colliders)
            {
                foreach (Collider otherCollider in colliders)
                {
                    if (otherCollider == ownCollider) continue;
                    CheckCollision(ownCollider, otherCollider);
                }
            }
        }

        private void CheckCollision(Collider ownCollider, Collider otherCollider)
        {
            Hit hit;
            if (!ownCollider.HitTest(otherCollider, out hit)) return;

            Assert.IsFalse(hit.normal.isZero);

            GameObject otherGameObject = otherCollider.gameObject;
            Rigidbody  otherRigidbody  = otherGameObject.physics.rigidbody;

            if (ownCollider.isTrigger || otherCollider.isTrigger)
            {
                new TriggerEvent(ownCollider, otherCollider).Post();
                return;
            }

            new CollisionEvent(
                gameObject     , ownCollider  , this,
                otherGameObject, otherCollider, otherRigidbody
            ).Post();

            // Resolve

            gameObject.position += hit.delta;

            var otherVelocity = Vector2.zero;
            float otherInverseMass = 0f;
            if (otherRigidbody != null)
            {
                otherVelocity = otherRigidbody.velocity;
                otherInverseMass = otherRigidbody.inverseMass;
            }

            float bounciness = Mathf.Min(ownCollider.bounciness, otherCollider.bounciness);

            CollisionResolutionHelper.Resolve(
                ref velocity, inverseMass,
                ref otherVelocity, otherInverseMass, 
                hit.normal, bounciness
            );
        }
    }
}