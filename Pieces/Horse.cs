namespace Board
{
    public class Horse : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row + i + 1;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row - i - 1;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row + i + 1;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row - i - 1;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            world.Create(new Location(piece.row, piece.column + 1), piece);
            world.Create(new Location(piece.row + 1, piece.column), piece);
            world.Create(new Location(piece.row, piece.column - 1), piece);
            world.Create(new Location(piece.row - 1, piece.column), piece);
        }
    }
}
