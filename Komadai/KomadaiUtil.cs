using System;
using UnityEngine;

namespace Board
{
    public class KomadaiUtil
    {
        const float cornerX = 3.3f, cornerY = 10.5f;
        public static void UpdatePosition(PieceList pieces, bool opposed)
        {
            if (pieces.Count <= 16)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    var piece = pieces[i];
                    piece.target = Position(pieces.Count, i, opposed);
                    piece.activated = true;
                }
            }
            else
            {
                var types = Enum.GetValues(typeof(PieceType));
                var counts = new int[types.Length];
                foreach (var piece in pieces)
                {
                    counts[(int)piece.type]++;
                }
                int j = 0;
                foreach (PieceType type in types)
                {
                    var count = counts[(int)type];
                    for (int i = 0; i < count; i++)
                    {
                        var piece = pieces[j++];
                        piece.target = Position(type, i, count, opposed);
                        piece.activated = true;
                    }
                    if (j == pieces.Count)
                        break;
                }
            }
        }

        static Vector3 Position(PieceType type, int i, int count, bool opposed)
        {
            var v = Calc(type, i, count);
            if (opposed)
                return PieceModel.Position(10 - v.X, 10 - v.Y);
            return PieceModel.Position(v.X, v.Y);
        }

        static Vector3 Position(int count, int i, bool opposed)
        {
            var v = Calc(i, count);
            if (opposed)
                return PieceModel.Position(10 - v.X, 10 - v.Y);
            return PieceModel.Position(v.X, v.Y);
        }
        public static Vector2 Calc(int i, int count)
        {
            var x = CalcX(i, count);
            var y = CalcY(i, count);
            return new Vector2(x, y);
        }

        public static Vector2 Calc(PieceType type, int i, int count)
        {
            var x = CalcX(type, i, count);
            var y = CalcY(type, i, count);
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
            if (13 <= count && count <= 15)
            {
                var r = count - 12;
                if (i < r)
                    return CalcX(0, 1) + 0.5f * (r - 1) - i;
                return CalcX(i - r, 16);
            }
            if (count <= 16)
                return 3.3f - i % 4;
            return 0.3f + i / 4;
        }

        static float CalcY(int i, int count)
        {
            if (count <= 2)
                return 12;
            if (count == 3)
            {
                if (i == 0)
                    return CalcY(4, 16);
                return CalcY(0, 3) + 1;
            }
            if (count <= 4)
                return CalcY(4 * (1 + i / 2), 16);
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
            {
                return CalcY(0, 1) - 1 + i / 3;
            }
            if (10 <= count && count <= 12)
            {
                var r = count - 8;
                if (i < r)
                    return CalcY(0, 9);
                return CalcY(0, 1) + (i - r) / 4;
            }
            if (13 <= count && count <= 15)
            {
                var r = count - 12;
                if (i < r)
                    return CalcY(0, 16);
                return CalcY((i - r) + 4, 16);
            }
            if (count <= 16)
                return CalcY(0, 1) - 1.5f + i / 4;
            return 10.7f + i % 4;
        }

        static float CalcX(PieceType type, int i, int count)
        {
            if (count == 1)
                return (CalcX(type, 0, 2) + CalcX(type, 1, 2)) / 2;

            if (type == PieceType.King)
                return cornerX - i / 3.0f;
            if (type == PieceType.Rook)
                return cornerX - 1 - (i + 1) / 3.0f;
            if (type == PieceType.Bishop)
                return cornerX - 2 - (2 + i) / 3.0f;
            if (type == PieceType.GoldGeneral)
                return cornerX - i / (count - 1.0f);
            if (type == PieceType.SilverGeneral || type == PieceType.Lance)
                return cornerX - 2 - i / (count - 1.0f);
            if (type == PieceType.Knight)
                return cornerX - i / (count - 1.0f);
            return cornerX - i * 3 / (count - 1.0f);
        }

        static float CalcY(PieceType type, int i, int count)
        {
            if (type == PieceType.King || type == PieceType.Rook || type == PieceType.Bishop)
                return cornerY;
            if (type == PieceType.GoldGeneral || type == PieceType.SilverGeneral)
                return cornerY + 1;
            if (type == PieceType.Knight || type == PieceType.Lance)
                return cornerY + 2;
            return cornerY + 3;
        }
    }
}
