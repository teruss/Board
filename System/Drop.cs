using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public class Drop : Command
    {
        World world;

        public Drop(PieceModel piece, World world) : base(world, piece)
        {
            this.world = world;
        }

        public override void Execute()
        {
            Piece.GetDropped(world);
        }

        public override void Undo()
        {
            world.GetKomadai(Piece.Player).Accept(Piece);
            base.Undo();
        }

        public override string ToString()
        {
            return "<drop>" + JsonUtility.ToJson(this) + "</drop>";
        }
    }
}
