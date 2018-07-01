using System;
using Engine;
using NUnit.Framework;

namespace Pong
{
    public class FsmState<T> : Component where T : Component
    {
        protected T agent;

        public bool isEntered { get; private set; }

        public void SetAgent(T agent)
        {
            Assert.IsNull(this.agent, this + ": agent is already set.");
            this.agent = agent;
        }

        public virtual void Enter()
        {
            Assert.IsFalse(isEntered, this + " is already entered. Can't Enter state.");

            Console.WriteLine("entered state:" + this);

            isEntered = true;
        }

        public virtual void Exit()
        {
            Assert.IsTrue(isEntered, this + " is not entered. Can't Exit state.");

            Console.WriteLine("exited state:" + this);

            isEntered = false;
        }
    }
}

