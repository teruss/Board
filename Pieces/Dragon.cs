namespace Board
{
    public class Dragon : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column, r = piece.row - i - 1;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column, r = piece.row + i + 1;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            Create(world, piece.column + 1, piece.row + 1, piece);
            Create(world, piece.column + 1, piece.row - 1, piece);
            Create(world, piece.column - 1, piece.row + 1, piece);
            Create(world, piece.column - 1, piece.row - 1, piece);
        }
    }
}
