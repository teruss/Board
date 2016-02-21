using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class Komadai
    {
        bool opposed;

        List<IPiece> pieces = new List<IPiece>();

        public Komadai(bool opposed)
        {
            this.opposed = opposed;
        }

        public void Accept(Piece piece)
        {
            pieces.Add(piece);
            UpdatePosition();
        }

        public void Drop(Piece piece)
        {
            pieces.Remove(piece);
            UpdatePosition();
        }

        void UpdatePosition()
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                pieces[i].target = Position(pieces[i], i);
                pieces[i].activated = true;
            }
        }

        Vector3 Position(IPiece piece, int i)
        {
            var x = CalcX(piece, i);
            var y = CalcY(piece, i);
            if (opposed)
                return piece.Position(10 - x, 10 - y);
            return piece.Position(x, y);
        }

        float CalcX(IPiece piece, int i)
        {
            if (pieces.Count <= 9)
                return 2.8f - i % 3;
            if (pieces.Count <= 16)
                return 3.2f - i % 4;
            return 0.2f + i / 4;
        }

        float CalcY(IPiece piece, int i)
        {
            if (pieces.Count <= 9)
                return 11.2f + i / 3;
            if (pieces.Count <= 16)
                return 10.5f + i / 4;
            return 10.5f + i % 4;
        }
    }
}
