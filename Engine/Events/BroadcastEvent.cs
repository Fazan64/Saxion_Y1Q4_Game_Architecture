using System;
using NUnit.Framework;

namespace Engine
{
    internal interface IBroadcastEvent
    {
        void Deliver();
    }

    public abstract class BroadcastEvent<T> : IBroadcastEvent where T : BroadcastEvent<T>
    {
        public delegate void Handler(T eventData);
        public static event Handler OnDeliver;

        private bool isPosted;
        private bool isDelivered;

        public void Post()
        {
            Assert.IsFalse(isPosted, $"{this} has already been posted!");
            Game.main.Post(this);
            isPosted = true;
        }

        void IBroadcastEvent.Deliver()
        {
            Assert.IsFalse(isDelivered, $"{this} has already been delivered!");
            OnDeliver?.Invoke((T)this);
            isDelivered = true;;
        }
    }

    public class TestEvent : BroadcastEvent<TestEvent>
    {
        public readonly string testData;

        public TestEvent(string testData)
        {
            this.testData = testData;
        }
    }
}
