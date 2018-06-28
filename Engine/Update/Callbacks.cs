using System;
using System.Reflection;

namespace Engine.Internal
{
    /// Provides the callbacks (Start, Update, OnCollision, OnTrigger) of a given object.
    /// Uses lazy initialization of delegates.
    internal class Callbacks
    {
        private readonly Object host;

        private readonly Lazy<Action> startLazy;
        private readonly Lazy<Action> updateLazy;

        private readonly Lazy<Action<Collision>> onCollisionLazy;
        private readonly Lazy<Action<Collider>>  onTriggerLazy;

        public Action start  => startLazy.Value;
        public Action update => updateLazy.Value;

        public Action<Collision> onCollision => onCollisionLazy.Value;
        public Action<Collider>  onTrigger   => onTriggerLazy.Value;

        public Callbacks(Object host)
        {
            this.host = host;

            startLazy = new Lazy<Action>(
                () => host.GetDelegate<Action>(EventFunctionNames.Start),
                isThreadSafe: false
            );

            updateLazy = new Lazy<Action>(
                () => host.GetDelegate<Action>(EventFunctionNames.FixedUpdate),
                isThreadSafe: false
            );

            onCollisionLazy = new Lazy<Action<Collision>>(
                () => host.GetDelegate<Action<Collision>>(EventFunctionNames.Collision),
                isThreadSafe: false
            );

            onTriggerLazy = new Lazy<Action<Collider>>(
                () => host.GetDelegate<Action<Collider>>(EventFunctionNames.Trigger),
                isThreadSafe: false
            );
        }
    }
}
