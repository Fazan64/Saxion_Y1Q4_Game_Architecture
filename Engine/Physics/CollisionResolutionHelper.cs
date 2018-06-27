using System;
using static Engine.Mathf;

namespace Engine.Internal
{
    public static class CollisionResolutionHelper
    {
        public static void Resolve(
            ref Vector2 aVelocity, float aInverseMass,
            ref Vector2 bVelocity, float bInverseMass,
            Vector2 normal, float bounciness
        )
        {
            float aSpeedAlongNormal = normal.Dot(aVelocity);
            float bSpeedAlongNormal = normal.Dot(bVelocity);

            if (aSpeedAlongNormal - bSpeedAlongNormal > 0f) return;

            float u =
                (aSpeedAlongNormal * bInverseMass + bSpeedAlongNormal * aInverseMass) /
                (aInverseMass + bInverseMass);

            float aDeltaSpeedAlongNormal = -(1f + bounciness) * (aSpeedAlongNormal - u);
            aVelocity += normal * aDeltaSpeedAlongNormal;

            float bDeltaSpeedAlongNormal = -(1f + bounciness) * (bSpeedAlongNormal - u);
            bVelocity += normal * bDeltaSpeedAlongNormal;
        }
    }
}