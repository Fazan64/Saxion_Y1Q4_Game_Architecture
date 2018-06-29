using System;

namespace Engine
{

    /// <summary>
    /// Contains several functions for doing floating point Math
    /// </summary>
    public static class Mathf
    {
        /// The Pi constant
        public const float PI = (float)Math.PI;

        // The golden ratio
        public const float Phi = 1.61803399f;

        /// Multiply by this to get radians from degrees 
        public const float degToRad = PI / 180f;

        /// Multiply by this to get degrees from radians 
        public const float radToDeg = 1f / degToRad;

        /// <summary>
        /// Returns the absolute value of specified number
        /// </summary>
        public static int Abs(int value)
        {
            return (value < 0) ? -value : value;
        }

        /// <summary>
        /// Returns the absolute value of specified number
        /// </summary>
        public static float Abs(float value)
        {
            return (value < 0) ? -value : value;
        }

        /// <summary>
        /// Returns the acosine of the specified number
        /// </summary>
        public static float Acos(float f)
        {
            return (float)Math.Acos(f);
        }

        /// <summary>
        /// Returns the arctangent of the specified number
        /// </summary>
        public static float Atan(float f)
        {
            return (float)Math.Atan(f);
        }

        /// <summary>
        /// Returns the angle whose tangent is the quotent of the specified values
        /// </summary>
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        /// <summary>
        /// Returns the smallest integer bigger greater than or equal to the specified number
        /// </summary>
        public static int Ceiling(float a)
        {
            return (int)Math.Ceiling(a);
        }

        /// <summary>
        /// Returns the cosine of the specified number
        /// </summary>
        public static float Cos(float f)
        {
            return (float)Math.Cos(f);
        }

        /// <summary>
        /// Returns the hyperbolic cosine of the specified number
        /// </summary>
        public static float Cosh(float value)
        {
            return (float)Math.Cosh(value);
        }

        /// <summary>
        /// Returns e raised to the given number
        /// </summary>
        public static float Exp(float f)
        {
            return (float)Math.Exp(f);
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified value
        /// </summary>
        public static int Floor(float f)
        {
            return (int)Math.Floor(f);
        }

        /// <summary>
        /// Returns the natural logarithm of the specified number
        /// </summary>
        public static float Log(float f)
        {
            return (float)Math.Log(f);
        }

        /// <summary>
        /// Returns the log10 of the specified number
        /// </summary>
        public static float Log10(float f)
        {
            return (float)Math.Log10(f);
        }

        /// <summary>
        /// Returns the biggest of the two specified values
        /// </summary>
        public static float Max(float value1, float value2)
        {
            return (value2 > value1) ? value2 : value1;
        }

        /// <summary>
        /// Returns the biggest of the two specified values
        /// </summary>
        public static int Max(int value1, int value2)
        {
            return (value2 > value1) ? value2 : value1;
        }

        /// <summary>
        /// Returns the smallest of the two specified values
        /// </summary>
        public static float Min(float value1, float value2)
        {
            return (value2 < value1) ? value2 : value1;
        }

        /// <summary>
        /// Returns the smallest of the two specified values
        /// </summary>
        public static int Min(int value1, int value2)
        {
            return (value2 < value1) ? value2 : value1;
        }

        /// <summary>
        /// Returns x raised to the power of y
        /// </summary>
        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }

        /// <summary>
        /// Returns the nearest integer to the specified value
        /// </summary>
        public static int Round(float f)
        {
            return (int)Math.Round(f);
        }

        /// <summary>
        /// Returns a value indicating the sign of the specified number (-1=negative, 0=zero, 1=positive)
        /// </summary>
        public static int Sign(float f)
        {
            if (f < 0f) return -1;
            if (f > 0f) return 1;
            return 0;
        }

        /// <summary>
        /// Returns a value indicating the sign of the specified number (-1=negative, 0=zero, 1=positive)
        /// </summary>
        public static int Sign(int f)
        {
            if (f < 0) return -1;
            if (f > 0) return 1;
            return 0;
        }

        /// <summary>
        /// Returns the sine of the specified number
        /// </summary>
        public static float Sin(float f)
        {
            return (float)Math.Sin(f);
        }

        /// <summary>
        /// Returns the hyperbolic sine of the specified number
        /// </summary>
        public static float Sinh(float value)
        {
            return (float)Math.Sinh(value);
        }

        /// <summary>
        /// Returns the square root of the specified number
        /// </summary>
        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt(f);
        }

        /// <summary>
        /// Returns the tangent of the specified number
        /// </summary>
        public static float Tan(float f)
        {
            return (float)Math.Tan(f);
        }

        /// <summary>
        /// Returns the hyperbolic tangent of the specified number
        /// </summary>
        public static float Tanh(float value)
        {
            return (float)Math.Tanh(value);
        }

        /// <summary>
        /// Calculates the integral part of the specified number
        /// </summary>
        public static float Truncate(float f)
        {
            return (float)Math.Truncate(f);
        }

        /// Linear interpolate between two given points (a and b).
        public static float Lerp(float a, float b, float t)
        {

            return a + (b - a) * t;
        }

        /// The inverse of Lerp.
        public static float InverseLerp(float a, float b, float p)
        {

            return (p - a) / (b - a);
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {

            return new Vector2(Lerp(a.x, b.x, t), Lerp(a.y, b.y, t));
        }

        public static float Map(float aMin, float aMax, float bMin, float bMax, float value)
        {

            return Lerp(bMin, bMax, InverseLerp(aMin, aMax, value));
        }

        /*public static Vector2 Map(Rectangle a, Rectangle b, Vector2 value) {

            return new Vector2(
                Map(a.min.x, a.max.x, b.min.x, b.max.x, value.x),
                Map(a.min.y, a.max.y, b.min.y, b.max.y, value.y)
            );
        }*/

        public static Vector2 Abs(Vector2 vec)
        {

            return new Vector2(Abs(vec.x), Abs(vec.y));
        }

        /// Clamps a given value to a given range.
        public static float Clamp(float value, float min, float max)
        {

            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public static float MoveTowards(float current, float target, float maxDelta)
        {
            float delta = target - current;
            if (Abs(delta) <= maxDelta) return target;
            if (delta < 0f) return current - maxDelta;
            return current + maxDelta;
        }
    }
}

