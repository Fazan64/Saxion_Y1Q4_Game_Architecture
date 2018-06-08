using System;
using NUnit.Framework;

namespace Engine
{
    public class EngineObject
    {
        public bool isDestroyed { get; private set; }

        public void Destroy()
        {
            Assert.IsFalse(isDestroyed, this + " is already destroyed!");

            isDestroyed = true;
            Game.main.Remove(this);
        }
    }
}
