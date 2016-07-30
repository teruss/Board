namespace Board
{
    public struct Vector2
    {
        public float X, Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Vector2 operator /(Vector2 lhs, float rhs)
        {
            return new Vector2(lhs.X / rhs, lhs.Y / rhs);
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }
    }
}
