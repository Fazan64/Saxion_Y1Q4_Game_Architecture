using System;

namespace Engine
{
    /// TODO What's returned by collider.CheckCollision();
    public class Hit
    {
        public Collider collider;
        public Vector2 normal;
        public Vector2 delta;
        public Vector2 position;
        public float timeOfImpact;

        public Hit() {}

        public Hit(Collider collider, Vector2 normal, float timeOfImpact)
        {
            this.collider = collider;
            this.normal = normal;
            this.timeOfImpact = timeOfImpact;
        }
    }
}
