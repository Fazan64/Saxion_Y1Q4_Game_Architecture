using System;

namespace Engine
{
    public class Collision
    {
        public readonly GameObject collidee;

        public Collision(GameObject collidee)
        {
            this.collidee = collidee;
        }
    }
}
