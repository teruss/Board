namespace Board
{
    public class Bishop : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row + i + 1;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row - i - 1;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row + i + 1;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row - i - 1;
                Create(world, c, r, piece);
                if (world.HasPiece(c, r))
                {
                    break;
                }
            }
        }
    }
}
