using System;
using static Engine.Mathf;

namespace Engine.Internal
{
    /// A collection of collision detection functions with 
    /// no dependencies on any of the Collider classes (for portability).
    internal static class CollisionDetectionHelper
    {
        /*
        public static Sweep CircleVsSegmentDiscrete(
            Vector2 circlePosition, float circleRadius,
            Vector2 segmentStart, Vector2 segmentEnd,
            GameObject collidee
        )
        {
            Vector2 segmentDelta = segmentEnd - segmentStart;
            Vector2 normal = segmentDelta.Normal();

            Vector2 fromStart = circlePosition - segmentStart;
            if (normal.Dot(fromStart) < 0f) normal *= -1f;
            float distance = fromStart.Dot(normal) - circleRadius;

            if (distance > 0f) return null;

            float distanceAlongLine = segmentDelta.normalized.Dot(fromStart);
            if (distanceAlongLine < 0f || distanceAlongLine > segmentDelta.magnitude) return null;

            return new Sweep(
                collidee: collidee,
                normal: normal,
                timeOfImpact: 0f
            );
        }

        public static Sweep CircleVsSegment(
            Vector2 moverPosition, float moverRadius, Vector2 movement,
            Vector2 segmentStart, Vector2 segmentEnd,
            GameObject collidee
        )
        {
            Vector2 fromStart = moverPosition - segmentStart;

            Vector2 segmentDelta = segmentEnd - segmentStart;
            Vector2 normal = segmentDelta.Normal();
            if (normal.Dot(fromStart) < 0f) normal *= -1f;

            float a = fromStart.Dot(normal) - moverRadius;
            float b = -normal.Dot(movement);

            if (b < 0f) return null; // if moving away.

            float t = a > 0f ? a / b : 0f;
            if (a < -moverRadius) return null; // If the starting position is already on the other side.

            if (t < 0f || t > 1f) return null;

            float distanceAlongLine = segmentDelta.normalized.Dot(fromStart + movement * t);
            if (distanceAlongLine < 0f || distanceAlongLine > segmentDelta.magnitude) return null;

            return new Sweep(
                collidee,
                normal,
                timeOfImpact: t
            );
        }

        public static Sweep CircleVsSegmentWithCaps(
            Vector2 moverPosition, float moverRadius, Vector2 movement,
            Vector2 segmentStart, Vector2 segmentEnd,
            GameObject collidee
        )
        {
            return
                CircleVsSegment(moverPosition, moverRadius, movement, segmentStart, segmentEnd, collidee) ??
                CircleVsCircle(moverPosition, moverRadius, movement, segmentStart, 0f, collidee) ??
                CircleVsCircle(moverPosition, moverRadius, movement, segmentEnd, 0f, collidee);
        }

        public static Sweep CircleVsCircle(
            Vector2 moverPosition, float moverRadius, Vector2 movement,
            Vector2 otherPosition, float otherRadius,
            GameObject collidee
        )
        {
            if (movement.isZero) return null;

            Vector2 relativePosition = moverPosition - otherPosition;

            float a = movement.sqrMagnitude;
            float b = 2f * relativePosition.Dot(movement);
            float c =
                relativePosition.sqrMagnitude -
                (moverRadius + otherRadius) * (moverRadius + otherRadius);

            // If moving out
            if (b >= 0f) return null;

            // If already overlapping.
            if (c < 0f)
            {
                return new Sweep(
                    normal: relativePosition.normalized,
                    collidee: collidee,
                    timeOfImpact: 0f
                );
            }

            float d = b * b - 4f * a * c;
            if (d < 0f) return null;

            float t = (-b - Sqrt(d)) / (2f * a);

            if (t < 0f) return null;
            if (t >= 1f) return null;

            return new Sweep(
                normal: (relativePosition + movement * t).normalized,
                collidee: collidee,
                timeOfImpact: t
            );
        }

        public static Sweep LineSegmentVsLineSegment(
            Vector2 startA, Vector2 endA,
            Vector2 startB, Vector2 endB,
            GameObject collidee
        )
        {
            return CircleVsSegment(
                startA, 0f, endA - startA,
                startB, endB,
                collidee
            );
        }
        */
    }
}