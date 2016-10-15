namespace Board
{
    public class Bishop : Move
    {
        public override bool IsPinnable
        {
            get
            {
                return true;
            }
        }

        public override void CreateMovable(World world, PieceModel piece)
        {
            var d = world.PieceManager.GetPinnedDirection(piece);
            if ((d & Direction.DownLeft) == 0)
                for (int i = 0; i < 8; i++)
                {
                    var l = Location.Create(piece.Location.Column + i + 1, piece.Location.Row + i + 1);
                    piece.CreateTraversableCell(world, l);
                    if (world.HasPiece(l))
                    {
                        break;
                    }
                }

            if ((d & Direction.UpLeft) == 0)
                for (int i = 0; i < 8; i++)
                {
                    var l = Location.Create(piece.Location.Column + i + 1, piece.Location.Row - i - 1);
                    piece.CreateTraversableCell(world, l);
                    if (world.HasPiece(l))
                    {
                        break;
                    }
                }

            if ((d & Direction.DownRight) == 0)
                for (int i = 0; i < 8; i++)
                {
                    var l = Location.Create(piece.Location.Column - i - 1, piece.Location.Row + i + 1);
                    piece.CreateTraversableCell(world, l);
                    if (world.HasPiece(l))
                    {
                        break;
                    }
                }

            if ((d & Direction.UpRight) == 0)
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
    }
}
