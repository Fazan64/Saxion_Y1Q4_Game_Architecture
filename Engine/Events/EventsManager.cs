using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NUnit.Framework;

namespace Engine
{
    internal class EventsManager
    {
        private readonly Queue<IBroadcastEvent> events = new Queue<IBroadcastEvent>();
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
            events.Enqueue(engineEvent);
        }

        public void DeliverEvents()
        {
            foreach (IBroadcastEvent engineEvent in events) engineEvent.Deliver();
            events.Clear();
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
