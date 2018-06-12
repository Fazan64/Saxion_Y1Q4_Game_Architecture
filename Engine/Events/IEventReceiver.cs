using System;

namespace Engine
{
    public interface IEventReceiver<T> where T : EngineEvent<T>
    {
        void On(T eventData);
    }
}
