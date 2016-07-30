namespace Board
{
    public class KomadaiUtil
    {
        public static Vector2 Calc(int i, int count)
        {
            var x = CalcX(i, count);
            var y = CalcY(i, count);
            return new Vector2(x, y);
        }

        static float CalcX(int i, int count)
        {
            if (count == 1)
                return 1.8f;
            if (count == 2 || count == 4)
                return 2.3f - i % 2;
            if (count <= 9)
                return 2.8f - i % 3;
            if (count <= 16)
                return 3.3f - i % 4;
            return 0.3f + i / 4;
        }

        static float CalcY(int i, int count)
        {
            if (count <= 3)
                return 12.2f;
            if (count == 4)
                return 11.7f + i / 2;
            if (count <= 9)
                return 11.2f + i / 3;
            if (count <= 16)
                return 10.7f + i / 4;
            return 10.7f + i % 4;
        }
    }
}
