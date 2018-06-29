using System;

namespace Engine
{
    /// A non-rotateable rectangle.
    public struct Rect
    {
        public float x, y, width, height;

        public float minX => x;
        public float maxX => x + width;
        public float minY => y;
        public float maxY => y + height;

        public Vector2 minXMinY => new Vector2(minX, minY);
        public Vector2 maxXMinY => new Vector2(maxX, minY);
        public Vector2 maxXMaxY => new Vector2(maxX, maxY);
        public Vector2 minXMaxY => new Vector2(minX, maxY);
        public Vector2[] corners => new[] 
        {
            minXMinY,
            minXMaxY,
            maxXMinY,
            maxXMaxY,
        };

        public Vector2 min
        {
            get => new Vector2(x, y);
            set 
            {
                x = value.x;
                y = value.y;
            }
        }

        public Vector2 max
        {
            get => new Vector2(x + width, y + height);
            set 
            {
                width  = value.x - x;
                height = value.y - y;
            }
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
            x = min.x;
            y = min.y;
            width  = max.x - min.x;
            height = max.y - min.y;
        }

        public Rect(Vector2 size)
        {
            x = y = 0f;
            width  = size.x;
            height = size.y;
        }

        public bool Overlaps(Rect other)
        {
            if (minX > other.maxX) return false;
            if (maxX < other.minX) return false;
            if (minY > other.maxY) return false;
            if (maxY < other.minY) return false;

            return true;
        }

        public bool Contains(Vector2 point)
        {
            return
                (point.x > minX &&
                 point.x < maxX &&
                 point.y > minY &&
                 point.y < maxY);
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
