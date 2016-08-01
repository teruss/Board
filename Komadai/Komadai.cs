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
            KomadaiUtil.UpdatePosition(pieces, opposed);
        }

        public void Drop(PieceModel piece)
        {
            pieces.Remove(piece);
            KomadaiUtil.UpdatePosition(pieces, opposed);
        }
    }
}
