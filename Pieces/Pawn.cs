using System;
using UnityEngine.Assertions;

namespace Board
{
    public class Pawn : Move
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
            if (d != Direction.AnyWhere)
            {
                return;
            }
            var l = piece.Location;
            switch (piece.Player)
            {
                case Player.Black:
                    piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row + 1));
                    break;
                case Player.White:
                    piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row - 1));
                    break;
                default:
                    Assert.AreNotEqual(Player.Gray, piece.Player);
                    break;
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, Location location)
        {
            if (!base.IsValid(gameController, piece, location))
                return false;

            foreach (var p in gameController.Pieces())
            {
                if (p != piece && !p.captured && !p.promoted && p.type == PieceType.Pawn && piece.Player == p.Player && p.Location.Column == location.Column)
                    return false;
            }

            switch (piece.Player)
            {
                case Player.Black:
                    return location.Row <= 8;
                case Player.White:
                    return location.Row >= 2;
                default:
                    Assert.AreNotEqual(Player.Gray, piece.Player);
                    return false;
            }
        }
    }
}
