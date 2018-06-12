using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NUnit.Framework;

namespace Engine
{
    internal class EventsManager
    {
        private readonly Queue<IEngineEvent> events = new Queue<IEngineEvent>();
        private readonly IEventReceiverRegisterer[] registerers;

        public EventsManager()
        {
            registerers = MakeRegisterers();
        }

        public void Add(EngineObject engineObject)
        {
            foreach (IEventReceiverRegisterer registerer in registerers)
            {
                if (registerer.Add(engineObject)) return;
            }
        }

        public void Remove(EngineObject engineObject)
        {
            foreach (IEventReceiverRegisterer registerer in registerers)
            {
                if (registerer.Remove(engineObject)) return;
            }
        }

        public void Post(IEngineEvent engineEvent)
        {
            events.Enqueue(engineEvent);
        }

        public void DeliverEvents()
        {
            foreach (IEngineEvent engineEvent in events) engineEvent.Deliver();
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
            Type eventBaseType = typeof(IEngineEvent);

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
            where TEvent : EngineEvent<TEvent>
        {
            public bool Add(EngineObject engineObject)
            {
                if (engineObject is IEventReceiver<TEvent> receiver)
                {
                    EngineEvent<TEvent>.handlers += receiver.On;
                    return true;
                }

                return false;
            }

            public bool Remove(EngineObject engineObject)
            {
                if (engineObject is IEventReceiver<TEvent> receiver)
                {
                    EngineEvent<TEvent>.handlers -= receiver.On;
                    return true;
                }

                return false;
            }
        }

        #endregion
    }
}
