﻿using System;
using NUnit.Framework;
using Engine.Internal;

namespace Engine
{
    public class EngineObject
    {
        public bool isDestroyed { get; private set; }

        protected Game game => Game.main;

        internal readonly Callbacks callbacks;

        public EngineObject()
        {
            callbacks = new Callbacks(this);
        }

        public void Destroy()
        {
            Assert.IsFalse(isDestroyed, this + " is already destroyed!");

            isDestroyed = true;
            Game.main.Remove(this);
        }
    }
}
