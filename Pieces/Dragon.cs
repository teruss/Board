namespace Board
{
    public class Dragon : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row;
                world.CreateTransversableCell(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column, r = piece.row - i - 1;
                world.CreateTransversableCell(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row;
                world.CreateTransversableCell(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column, r = piece.row + i + 1;
                world.CreateTransversableCell(new Location(r, c), piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            world.CreateTransversableCell(new Location(piece.row + 1, piece.column + 1), piece);
            world.CreateTransversableCell(new Location(piece.row - 1, piece.column + 1), piece);
            world.CreateTransversableCell(new Location(piece.row + 1, piece.column - 1), piece);
            world.CreateTransversableCell(new Location(piece.row - 1, piece.column - 1), piece);
        }
    }
}
