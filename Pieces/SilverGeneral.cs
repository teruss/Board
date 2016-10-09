using System;

namespace Board
{
    public class SilverGeneral : Move
    {
        public override bool IsPinnable
        {
            get
            {
                return false;
            }
        }
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            if (piece.Player == Player.Black)
                piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row + 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row + 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row + 1));
            if (piece.Player == Player.White)
                piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row - 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row - 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row - 1));
        }
    }
}
