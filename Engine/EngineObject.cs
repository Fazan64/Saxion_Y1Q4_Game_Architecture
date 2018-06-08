using System;
using NUnit.Framework;

namespace Engine
{
    public class EngineObject
    {
        public bool isDestroyed { get; private set; }

        public EngineObject()
        {
            Game.main.Add(this);
        }

        public static void Destroy(EngineObject engineObject)
        {
            Assert.IsFalse(engineObject.isDestroyed, engineObject + " is already destroyed!");

            engineObject.isDestroyed = true;
            Game.main.Remove(engineObject);
        }
    }
}
