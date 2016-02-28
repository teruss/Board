using UnityEngine.Assertions;

namespace Board
{
    public class Lance : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            switch (piece.Player)
            {
                case Player.Black:
                    for (int i = 0; i < 8; i++)
                    {
                        var loc = Location.Create(l.Column, l.Row + i + 1);
                        piece.CreateTraversableCell(world, loc);
                        if (world.HasPiece(loc))
                        {
                            break;
                        }
                    }
                    break;
                case Player.White:
                    for (int i = 0; i < 8; i++)
                    {
                        var loc = Location.Create(l.Column, l.Row - i - 1);
                        piece.CreateTraversableCell(world, loc);
                        if (world.HasPiece(loc))
                        {
                            break;
                        }
                    }
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
