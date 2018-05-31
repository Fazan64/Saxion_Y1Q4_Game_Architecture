using System;

namespace Spaghetti
{
    using static Mathf;

    [Serializable]
    public struct Vector2
    {
        public static readonly Vector2 zero  = new Vector2(0f, 0f);
        public static readonly Vector2 one   = new Vector2(1f, 1f);
        public static readonly Vector2 right = new Vector2(1f, 0f);
        public static readonly Vector2 up    = new Vector2(0f, 1f);
        public static readonly Vector2 left = -right;
        public static readonly Vector2 down = -up;
        public static readonly Vector2 half = one * 0.5f;

        public float x;
        public float y;

        public bool isZero
        {
            get
            {
                return
                    Abs(x) < 0.001f &&
                    Abs(y) < 0.001f;
            }
        }

        public Vector2 normalized => isZero ? zero : this / magnitude;

        public float magnitude => Sqrt(x * x + y * y);
        public float sqrMagnitude => x * x + y * y;

        /// The angle of the vector in radians.
        public float angle
        {
            get
            {
                float result = Atan2(y, x);
                if (result < 0f) result += 2f * PI;

                return result;
            }
        }

        public static float Angle(Vector2 a, Vector2 b)
        {
            return Acos(Dot(a, b) / a.magnitude / b.magnitude);
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector2 FromAngleAndLength(float angle, float length)
        {
            return new Vector2(
                Cos(angle),
                Sin(angle)
            ) * length;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Add(Vector2 other) => this += other;
        public Vector2 Subtract(Vector2 other) => this -= other;
        public Vector2 Scale(Vector2 other)
        {
            this.x *= other.x;
            this.y *= other.y;
            return this;
        }

        public Vector2 TruncatedBy(float maxLength)
        {
            if (this.magnitude <= maxLength) return this;
            return normalized * maxLength;
        }

        public Vector2 ScaledBy(Vector2 other)
        {
            return new Vector2(x * other.x, y * other.y);
        }

        public Vector2 DividedBy(Vector2 other)
        {
            return new Vector2(x / other.x, y / other.y);
        }

        /// Returns a copy of this vector rotated by the given angle in radians.
        public Vector2 RotatedBy(float angle)
        {
            return FromAngleAndLength(this.angle + angle, magnitude);
        }

        public float GetAngleRadians() => angle;
        public float GetAngleDegrees() => angle * radToDeg;

        public Vector2 SetAngleRadians(float newAngle) => RotateRadians(newAngle - angle);
        public Vector2 SetAngleDegrees(float newAngle) => SetAngleRadians(newAngle * degToRad);

        public Vector2 RotateRadians(float rotationAngle)
        {
            float cos = Cos(rotationAngle);
            float sin = Sin(rotationAngle);

            float newX = cos * x - sin * y;
            float newY = sin * x + cos * y;

            x = newX;
            y = newY;

            return this;
        }

        public Vector2 RotateDegrees(float rotationAngle)
        {
            return RotateRadians(rotationAngle * degToRad);
        }

        public Vector2 RotateAroundRadians(float rotationAngle, Vector2 rotationPoint)
        {
            return this
                .Subtract(rotationPoint)
                .RotateRadians(rotationAngle)
                .Add(rotationPoint);
        }

        public Vector2 RotateAroundDegrees(float rotationAngle, Vector2 rotationPoint)
        {
            return RotateAroundRadians(rotationAngle * degToRad, rotationPoint);
        }

        public Vector2 Normalize()
        {
            if (isZero) return this;

            float invMagnitude = 1f / magnitude;
            x *= invMagnitude;
            y *= invMagnitude;
            return this;
        }

        public Vector2 Clone() => new Vector2(x, y);

        public Vector2 SetXY(float x, float y)
        {
            this.x = x;
            this.y = y;

            return this;
        }

        public Vector2 SetXY(Vector2 other)
        {
            x = other.x;
            y = other.y;

            return this;
        }

        public Vector2 Reflect(Vector2 surfaceNormal, float coeficientOfReflection = 1f)
        {
            Vector2 unitNormal = surfaceNormal.normalized;
            return Subtract(
                unitNormal * (1f + coeficientOfReflection) * Dot(this, unitNormal)
            );
        }

        public Vector2 ProjectedOn(Vector2 other)
        {
            Vector2 unitOther = other.normalized;
            return unitOther * Dot(this, unitOther);
        }

        /// Returns a copy of this vector rotated by the given angle in radians.
        public Vector2 RotatedByRadians(float angle) => Clone().RotateRadians(angle);

        /// Returns a unit normal to this vector (rotated +90deg)
        public Vector2 Normal() => new Vector2(-y, x).normalized;

        public float Dot(Vector2 other) => Dot(this, other);

        override public string ToString()
        {
            return "[Vector2 " + x + ", " + y + "]";
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 vec, float scalar)
        {
            return new Vector2(vec.x * scalar, vec.y * scalar);
        }

        public static Vector2 operator /(Vector2 vec, float scalar)
        {
            return new Vector2(vec.x / scalar, vec.y / scalar);
        }

        public static Vector2 operator -(Vector2 vec)
        {
            return new Vector2(-vec.x, -vec.y);
        }

        /*public static explicit operator Vector2Int(Vector2 vec) {

            return new Vector2Int((int)vec.x, (int)vec.y);
        }*/

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return
                Abs(a.x - b.x) < float.Epsilon &&
                Abs(a.y - b.y) < float.Epsilon;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return
                Abs(a.x - b.x) >= float.Epsilon ||
                Abs(a.y - b.y) >= float.Epsilon;
        }
    }

    public static class Vector2Extensions
    {
        public static Vector2 GetClosestCardinalDirection(this Vector2 vec)
        {
            vec = vec.normalized;
            if (vec.y > vec.x)
                return vec.y > -vec.x ? Vector2.up : Vector2.left;
            else
                return vec.y > -vec.x ? Vector2.right : Vector2.down;
        }
    }
}

