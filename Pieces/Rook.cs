using System;

namespace Board
{
    public class Rook : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            for (int i = 0; i < 8; i++)
            {
                var loc = Location.Create(l.Column + i + 1, l.Row);
                piece.CreateTraversableCell(world, loc);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                var loc = Location.Create(l.Column, l.Row - i - 1);
                piece.CreateTraversableCell(world, loc);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                var loc = Location.Create(l.Column - i - 1, l.Row);
                piece.CreateTraversableCell(world, loc);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                var loc = Location.Create(l.Column, l.Row + i + 1);
                piece.CreateTraversableCell(world, loc);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
        }
    }
}
