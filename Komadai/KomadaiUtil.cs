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
            if (count == 3)
            {
                if (i < 1)
                    return CalcX(0, 1);
                return CalcX(i - 1, 2);
            }
            if (count == 5)
            {
                if (i < 2)
                    return CalcX(i, 2);
                return CalcX(0, 1) - i + 3;
            }
            if (count == 7)
            {
                if (i == 0)
                    return CalcX(0, 1);
                return CalcX(i - 1, 6);
            }
            if (count == 8)
            {
                if (i < 2)
                    return CalcX(i, 2);
                return CalcX(i - 2, 6);
            }
            if (count <= 9)
                return 2.8f - i % 3;
            if (count == 10)
            {
                if (i < 2)
                    return CalcX(i, 2);
                return CalcX(i - 2, 16);
            }
            if (count == 11)
            {
                if (i < 3)
                    return CalcX(i, 9);
                return CalcX(i - 3, 16);
            }
            if (count <= 16)
                return 3.3f - i % 4;
            return 0.3f + i / 4;
        }

        static float CalcY(int i, int count)
        {
            if (count <= 2)
                return 12.2f;
            if (count == 3)
            {
                if (i == 0)
                    return 11.7f;
                return CalcY(0, 3) + 1;
            }
            if (count <= 4)
                return 11.7f + i / 2;
            if (count == 5)
            {
                if (i < 2)
                    return CalcY(i, 4);
                return CalcY(2, 4);
            }
            if (count == 6)
            {
                if (i < 3)
                    return CalcY(0, 4);
                return CalcY(2, 4);
            }
            if (count == 7)
            {
                if (i == 0)
                    return CalcY(0, 1) - 1;
                return CalcY(0, 1) + (i - 1) / 3;
            }
            if (count == 8)
            {
                if (i == 0)
                    return CalcY(0, 7);
                return CalcY(i - 1, 7);
            }
            if (count <= 9)
                return 11.2f + i / 3;
            if (10 <= count && count <= 12)
            {
                var r = count - 8;
                if (i < r)
                    return CalcY(0, 9);
                return CalcY(0, 1) + (i - r) / 4;
            }
            if (count <= 16)
                return 10.7f + i / 4;
            return 10.7f + i % 4;
        }
    }
}
