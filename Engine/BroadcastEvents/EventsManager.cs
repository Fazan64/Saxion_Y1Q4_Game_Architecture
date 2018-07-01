using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NUnit.Framework;

namespace Engine.Internal
{
    /// <summary>
    /// Keeps track of broadcastable event receivers.
    /// Makes it possible to queue up posted broadcastable events and 
    /// deliver them all together to the appropriate receivers later.
    /// Add/Remove event receivers
    /// Post/Deliver events.
    /// </summary>
    internal class EventsManager
    {
        private readonly Queue<IBroadcastEvent> events = new Queue<IBroadcastEvent>();

        /// Each one of these is responsible for registering a receiver of events
        /// of a single concrete type. There's as many of these as there are concrete
        /// broadcastable event types.
        private readonly IEventReceiverRegisterer[] registerers;

        public EventsManager()
        {
            registerers = MakeRegisterers();
        }

        public void Add(EngineObject engineObject)
        {
            foreach (IEventReceiverRegisterer registerer in registerers)
            {
                registerer.Add(engineObject);
            }
        }

        public void Remove(EngineObject engineObject)
        {
            foreach (IEventReceiverRegisterer registerer in registerers)
            {
                registerer.Remove(engineObject);
            }
        }

        public void Post(IBroadcastEvent engineEvent)
        {
            Assert.IsNotNull(engineEvent);
            events.Enqueue(engineEvent);
        }

        /// Delivers all events posted since the last call to this method.
        /// Any events posted while delivering events will also be delivered.
        public void DeliverEvents()
        {
            while (events.Count > 0)
            {
                events.Dequeue().Deliver();
            }
        }

        #region Dark Arts

        private static IEventReceiverRegisterer[] MakeRegisterers()
        {
            return GetEventTypes()
                .Select(MakeRegistererForEventType)
                .ToArray();
        }

        private static IEnumerable<Type> GetEventTypes()
        {
            Type eventBaseType = typeof(IBroadcastEvent);

            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract && !type.IsInterface && eventBaseType.IsAssignableFrom(type));
        }

        private static IEventReceiverRegisterer MakeRegistererForEventType(Type eventType)
        {
            Type registererType = typeof(EventReceiverRegisterer<>).MakeGenericType(eventType);
            return (IEventReceiverRegisterer)Activator.CreateInstance(registererType);
        }

        /// Registers event receivers
        private interface IEventReceiverRegisterer
        {
            bool Add(EngineObject engineObject);
            bool Remove(EngineObject engineObject);
        }

        /// Registers event receivers of type TEvent, 
        /// i.e registers objects which implement IEventReceiver<TEvent>.
        private class EventReceiverRegisterer<TEvent> : IEventReceiverRegisterer
            where TEvent : BroadcastEvent<TEvent>
        {
            public bool Add(EngineObject engineObject)
            {
                if (engineObject is IEventReceiver<TEvent> receiver)
                {
                    BroadcastEvent<TEvent>.OnDeliver += receiver.On;
                    return true;
                }

                return false;
            }

            public bool Remove(EngineObject engineObject)
            {
                if (engineObject is IEventReceiver<TEvent> receiver)
                {
                    BroadcastEvent<TEvent>.OnDeliver -= receiver.On;
                    return true;
                }

                return false;
            }
        }

        #endregion
    }
}
