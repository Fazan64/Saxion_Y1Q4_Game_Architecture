﻿using System;
using Engine;
using NUnit.Framework;

namespace Pong
{
    /// Broadcasts a BallCollidedWithBooster event upon collision with a Ball.
    public class Booster : Component
    {
        void OnTrigger(Collider collider)
        {
            if (!collider.gameObject.Has<Ball>()) return;

            new BallBoostEvent(collider.gameObject).Post();
        }
    }
}
