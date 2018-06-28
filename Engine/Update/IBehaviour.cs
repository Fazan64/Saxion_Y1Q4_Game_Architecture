using System;
using Engine.Internal;

namespace Engine
{
    internal interface IBehaviour
    {
        Callbacks GetCallbacks();
    }
}
