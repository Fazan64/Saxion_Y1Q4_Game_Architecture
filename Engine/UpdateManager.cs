using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Engine
{
    public class UpdateManager
    {
        private Action startActions;
        private Action updateActions;

        private readonly HashSet<EngineObject> registered = new HashSet<EngineObject>();

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
                Add(behaviour.GetCallbacks());
                registered.Add(engineObject);
            }
        }

        public void Remove(EngineObject engineObject)
        {
            if (engineObject is IBehaviour behaviour)
            {
                Remove(behaviour.GetCallbacks());
            }
        }

        public bool Contains(EngineObject engineObject)
        {
            return registered.Contains(engineObject);
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

