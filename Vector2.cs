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

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }
    }
}
