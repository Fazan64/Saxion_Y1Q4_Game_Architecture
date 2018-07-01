using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace Engine.Internal
{
    /// Responsible for calling the Start and Update methods on
    /// added EngineObject|s.
    /// Step is intended to be called every fixed timestep.
    /// It executes Start on any EngineObject|s added since the last Step.
    /// Then executes Update of all added EngineObjects|s.
    internal class UpdateManager
    {
        private Action startActions;
        private Action updateActions;

        private readonly HashSet<EngineObject> registered = new HashSet<EngineObject>();

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
            Assert.That(registered, Has.No.Member(engineObject));

            Add(engineObject.callbacks);

            registered.Add(engineObject);
        }

        public void Remove(EngineObject engineObject)
        {
            Assert.That(registered, Has.Member(engineObject));

            Remove(engineObject.callbacks);

            registered.Remove(engineObject);
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

