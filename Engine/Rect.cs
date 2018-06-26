using System;

namespace Engine
{
    /// A non-rotateable rectangle.
    public struct Rect
    {
        public float x, y, width, height;

        public float left   => x;
        public float right  => x + width;
        public float top    => y;
        public float bottom => y + height;

        public Vector2 leftTop     => new Vector2(left , top);
        public Vector2 rightTop    => new Vector2(right, top);
        public Vector2 rightBottom => new Vector2(right, bottom);
        public Vector2 leftBottom  => new Vector2(left , bottom);
        public Vector2[] corners => new[] {
            leftTop,
            rightTop,
            rightBottom,
            leftBottom
        };

        public Vector2 min
        {
            get => new Vector2(x, y);
        }

        public Vector2 max
        {
            get => new Vector2(x + width, y + height);
        }

        public Vector2 diagonal
        {
            get => new Vector2(width, height);
            set
            {
                width  = value.x;
                height = value.y;
            }
        }

        public Vector2 halfDiagonal
        {
            get => new Vector2(width, height) * 0.5f;
            set
            {
                width  = value.x * 2f;
                height = value.y * 2f;
            }
        }

        public Vector2 center
        {
            get
            {
                return new Vector2(x + width * 0.5f, y + height * 0.5f);
            }
            set
            {
                x = value.x - width  * 0.5f;
                y = value.y - height * 0.5f;
            }
        }

        public static Rect FromCenterAndHalfDiagonal(Vector2 center, Vector2 halfDiagonal)
        {
            return new Rect
            {
                halfDiagonal = halfDiagonal,
                center = center
            };
        }

        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width  = width;
            this.height = height;
        }

        /// Makes a rectangle with the given topleft nad bottomright corners
        public Rect(Vector2 min, Vector2 max)
        {
            this.x = min.x;
            this.y = min.y;
            this.width  = max.x - min.x;
            this.height = max.y - min.y;
        }

        public Rect(Vector2 size)
        {
            x = y = 0f;
            width  = size.x;
            height = size.y;
        }

        public bool Overlaps(Rect other)
        {
            if (left   > other.right ) return false;
            if (right  < other.left  ) return false;
            if (top    > other.bottom) return false;
            if (bottom < other.top   ) return false;

            return true;
        }

        public bool Contains(Vector2 point)
        {
            return
                (point.x > left  &&
                 point.x < right &&
                 point.y > top   &&
                 point.y < bottom);
        }

        public void Inflate(float amount)
        {
            if (-amount * 2f > width ) amount = width  * -0.5f;
            if (-amount * 2f > height) amount = height * -0.5f;

            x -= amount;
            width  += 2f * amount;

            y -= amount;
            height += 2f * amount;
        }

        override public string ToString()
        {
            return "[Rectangle: " + x + ", " + y + ", " + width + ", " + height + "]";
        }
    }
}
