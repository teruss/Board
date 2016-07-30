using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class Komadai
    {
        bool opposed;

        PieceList pieces = new PieceList();

        public Komadai(bool opposed)
        {
            this.opposed = opposed;
        }

        public void Accept(PieceModel piece)
        {
            pieces.Add(piece);
            UpdatePosition();
        }

        public void Drop(PieceModel piece)
        {
            pieces.Remove(piece);
            UpdatePosition();
        }

        void UpdatePosition()
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                var piece = pieces[i];
                piece.target = Position(piece, i);
                piece.activated = true;
            }
        }

        Vector3 Position(PieceModel piece, int i)
        {
            var x = CalcX(piece, i);
            var y = CalcY(piece, i);
            if (opposed)
                return PieceModel.Position(10 - x, 10 - y);
            return PieceModel.Position(x, y);
        }

        float CalcX(PieceModel piece, int i)
        {
            if (pieces.Count <= 9)
                return 2.8f - i % 3;
            if (pieces.Count <= 16)
                return 3.2f - i % 4;
            return 0.2f + i / 4;
        }

        float CalcY(PieceModel piece, int i)
        {
            if (pieces.Count <= 9)
                return 11.2f + i / 3;
            if (pieces.Count <= 16)
                return 10.5f + i / 4;
            return 10.5f + i % 4;
        }
    }
}
