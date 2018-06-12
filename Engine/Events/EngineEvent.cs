using System;
using NUnit.Framework;

namespace Engine
{
    internal interface IEngineEvent
    {
        void Deliver();
    }

    public abstract class EngineEvent<T> : IEngineEvent where T : EngineEvent<T>
    {
        public delegate void Handler(T eventData);
        public static event Handler handlers;

        private bool isPosted;
        private bool isDelivered;

        public void Post()
        {
            Assert.IsFalse(isPosted, $"{this} has already been posted!");
            Game.main.Post(this);
            isPosted = true;
        }

        void IEngineEvent.Deliver()
        {
            Assert.IsFalse(isDelivered, $"{this} has already been delivered!");
            handlers?.Invoke((T)this);
            isDelivered = true;;
        }
    }

    public class TestEvent : EngineEvent<TestEvent>
    {
        public readonly string testData;

        public TestEvent(string testData)
        {
            this.testData = testData;
        }
    }

    public class CollisionEvent : EngineEvent<CollisionEvent>
    {
        public readonly GameObject a;
        public readonly GameObject b;
    }
}
