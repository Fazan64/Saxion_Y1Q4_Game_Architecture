using System;
using Engine;
using NUnit.Framework;

namespace Pong
{
    public class Paddle : Component
    {
        const float VerticalPadding = 4f;
        const float SizeY = 64f;

        void Update()
        {
            float minY = 0f               + VerticalPadding + SizeY * 0.5f;
            float maxY = game.size.y - 1f - VerticalPadding - SizeY * 0.5f;
            gameObject.position.y = Mathf.Clamp(gameObject.position.y, minY, maxY);
        }

        void OnCollision(Collision collision)
        {
            if (!collision.gameObject.Has<Ball>()) return;

            //float deltaY = Mathf.Abs(gameObject.position.y - collision.gameObject.position.y);
            //float normalizedDeltaY = deltaY / (64f * 0.5f);
            //Console.WriteLine("Hit " + normalizedDeltaY); // just for testing

            //float yMultiplier = 1.5f + normalizedDeltaY * 2f;

            float deltaY = Mathf.Abs(gameObject.position.y - SizeY - collision.gameObject.position.y);
            Console.WriteLine("Hit " + deltaY); // just for testing

            float yVelocityMultiplier = Mathf.Abs(deltaY) / 50f;

            Assert.IsNotNull(collision.rigidbody);
            collision.rigidbody.velocity.y *= yVelocityMultiplier;
        }
    }
}
