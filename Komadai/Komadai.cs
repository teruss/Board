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
            var v = KomadaiUtil.Calc(i, pieces.Count);
            if (opposed)
                return PieceModel.Position(10 - v.X, 10 - v.Y);
            return PieceModel.Position(v.X, v.Y);
        }
    }
}
