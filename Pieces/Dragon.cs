namespace Board
{
    public class Dragon : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column, r = piece.row - i - 1;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column, r = piece.row + i + 1;
                world.Create(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            world.Create(new Location(piece.row + 1, piece.column + 1), piece);
            world.Create(new Location(piece.row - 1, piece.column + 1), piece);
            world.Create(new Location(piece.row + 1, piece.column - 1), piece);
            world.Create(new Location(piece.row - 1, piece.column - 1), piece);
        }
    }
}
