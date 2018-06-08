using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace Engine
{
    public class UpdateManager
    {
        private Action startActions;
        private Action updateActions;

        private readonly HashSet<IBehaviour> registered = new HashSet<IBehaviour>();

        public void Step()
        {
            // Calling initializeActions may cause initializeActions.
            // Makes sure the additional Initialize calls are also called.
            while (startActions != null)
            {
                Action copy = startActions;
                startActions = null;
                copy?.Invoke();
            }

            updateActions?.Invoke();
        }

        public void Add(EngineObject engineObject)
        {
            if (engineObject is IBehaviour behaviour)
            {
                Assert.IsFalse(registered.Contains(behaviour));

                Add(behaviour.GetCallbacks());
                registered.Add(behaviour);
            }
        }

        public void Remove(EngineObject engineObject)
        {
            if (engineObject is IBehaviour behaviour)
            {
                Assert.IsTrue(registered.Contains(behaviour));

                Remove(behaviour.GetCallbacks());
                registered.Remove(behaviour);
            }
        }

        public bool Contains(EngineObject engineObject)
        {
            if (!(engineObject is IBehaviour behaviour)) return false; 
            return registered.Contains(behaviour);
        }

        private void Add(Callbacks callbacks)
        {
            startActions = (Action)Delegate.Combine(
                startActions,
                callbacks.start
            );

            updateActions = (Action)Delegate.Combine(
                updateActions,
                callbacks.update
            );
        }

        private void Remove(Callbacks callbacks)
        {
            startActions = (Action)Delegate.Remove(
                startActions,
                callbacks.start
            );

            updateActions = (Action)Delegate.Remove(
                updateActions,
                callbacks.update
            );
        }
    }
}

