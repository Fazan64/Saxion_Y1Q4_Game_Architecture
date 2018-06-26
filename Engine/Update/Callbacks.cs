using System;
using System.Reflection;

namespace Engine
{
    /// Provides the callbacks (Initialize, Update, OnCollision) of a given object.
    /// Uses lazy initialization of delegates.
    public class Callbacks
    {
        private readonly Object host;

        private readonly Lazy<Action> _start;
        private readonly Lazy<Action> _update;
        private readonly Lazy<Action<Collision>> _onCollision;

        public Action start => _start.Value;
        public Action update => _update.Value;
        public Action<Collision> onCollision => _onCollision.Value;

        public Callbacks(Object host)
        {
            this.host = host;

            _start = new Lazy<Action>(
                () => host.GetDelegate<Action>("Start"),
                isThreadSafe: false
            );

            _update = new Lazy<Action>(
                () => host.GetDelegate<Action>("Update"),
                isThreadSafe: false
            );

            _onCollision = new Lazy<Action<Collision>>(
                () => host.GetDelegate<Action<Collision>>("OnCollision"),
                isThreadSafe: false
            );
        }
    }
}
