using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace Engine.Internal
{
    /// Responsible for calling the Start and Update methods on
    /// added EngineObject|s.
    /// `Step` is intended to be called every fixed timestep.
    /// It executes Start on any EngineObject|s added since the last `Step`.
    /// Then executes `Update` of all added EngineObjects|s.
    internal class UpdateManager
    {
        private Action startActions;
        private Action updateActions;

        private readonly HashSet<IBehaviour> registered = new HashSet<IBehaviour>();

        public void Step()
        {
            // Calling startActions may cause more objects being created.
            // This makes sure the additional Start calls are also called.
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
                Assert.That(registered, Has.No.Member(behaviour));

                Add(behaviour.GetCallbacks());
                registered.Add(behaviour);
            }
        }

        public void Remove(EngineObject engineObject)
        {
            if (engineObject is IBehaviour behaviour)
            {
                Assert.That(registered, Has.Member(behaviour));

                Remove(behaviour.GetCallbacks());
                registered.Remove(behaviour);
            }
        }

        public bool Contains(EngineObject engineObject)
        {
            if (engineObject is IBehaviour behaviour)
            {
                return registered.Contains(behaviour);
            }

            return false;
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

