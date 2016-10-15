using System;
using UnityEngine.Assertions;

namespace Board
{
    public class Knight : Move
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
            var d = world.PieceManager.GetPinnedDirection(piece);
            if (d != PieceManager.Direction.None)
            {
                return;
            }
            var l = piece.Location;
            if (piece.Player == Player.White)
            {
                piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row - 2));
                piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row - 2));
            }
            else
            {
                piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row + 2));
                piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row + 2));
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, Location location)
        {
            if (!base.IsValid(gameController, piece, location))
                return false;

            switch (piece.Player)
            {
                case Player.Black:
                    return location.Row <= 7;
                case Player.White:
                    return location.Row >= 3;
                default:
                    Assert.AreNotEqual(Player.Gray, piece.Player);
                    return false;
            }
        }
    }
}
