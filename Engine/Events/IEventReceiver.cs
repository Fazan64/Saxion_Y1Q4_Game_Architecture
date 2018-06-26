using System;

namespace Engine
{
    public interface IEventReceiver<T> where T : BroadcastEvent<T>
    {
        void On(T eventData);
    }
}
