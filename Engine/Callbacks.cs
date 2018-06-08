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
        private readonly Lazy<Action<GameObject>> _onCollision;
        //readonly Lazy<Action<Collision>>  _onCollisionDetailed;

        // ICallbacks implementation
        public Action start => _start.Value;
        public Action update => _update.Value;
        public Action<GameObject> onCollision => _onCollision.Value;
        //public Action<Collision> onCollisionDetailed => _onCollisionDetailed.Value;

        public Callbacks(Object host)
        {
            this.host = host;

            _start = new Lazy<Action>(
                () => (Action)host.GetDelegate<Action>("Start"),
                isThreadSafe: false
            );

            _update = new Lazy<Action>(
                () => (Action)host.GetDelegate<Action>("Update"),
                isThreadSafe: false
            );

            _onCollision = new Lazy<Action<GameObject>>(
                () => (Action<GameObject>)host.GetDelegate<Action<GameObject>>("OnCollision"),
                isThreadSafe: false
            );

            /*_onCollisionDetailed = new Lazy<Action<Collision>>(
                () => (Action<Collision>)host.GetDelegate<Action<Collision>>("OnCollisionDetailed"),
                isThreadSafe: false
            );*/
        }
    }
}
