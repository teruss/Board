namespace Board
{
    public class Horse : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var d = world.PieceManager.GetPinnedDirection(piece);

            if ((d & Direction.Horizontal) != 0)
            {
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column + 1, piece.Location.Row));
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column - 1, piece.Location.Row));
            }

            if ((d & Direction.Vertical) != 0)
            {
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column, piece.Location.Row + 1));
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column, piece.Location.Row - 1));
            }

            if ((d & Direction.Slash) != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    var l = Location.Create(piece.Location.Column + i + 1, piece.Location.Row + i + 1);
                    piece.CreateTraversableCell(world, l);
                    if (world.HasPiece(l))
                    {
                        break;
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    var l = Location.Create(piece.Location.Column - i - 1, piece.Location.Row - i - 1);
                    piece.CreateTraversableCell(world, l);
                    if (world.HasPiece(l))
                    {
                        break;
                    }
                }
            }

            if ((d & Direction.BackSlash) != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    var l = Location.Create(piece.Location.Column + i + 1, piece.Location.Row - i - 1);
                    piece.CreateTraversableCell(world, l);
                    if (world.HasPiece(l))
                    {
                        break;
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    var l = Location.Create(piece.Location.Column - i - 1, piece.Location.Row + i + 1);
                    piece.CreateTraversableCell(world, l);
                    if (world.HasPiece(l))
                    {
                        break;
                    }
                }
            }
        }
    }
}
