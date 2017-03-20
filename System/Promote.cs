using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public class Promote : Command
    {
        public Promote(World world, PieceModel piece) : base(world, piece)
        {
        }

        public override void Execute()
        {
            Piece.SetPromoted(true);
        }

        public override string ToString()
        {
            return "<promote>" + JsonUtility.ToJson(this) + "</promote>";
        }
    }
}
