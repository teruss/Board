namespace Board
{
    public class Lance : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            if (piece.opposed)
            {
                for (int i = 0; i < 8; i++)
                {
                    int c = piece.column, r = piece.row + i + 1;
                    Create(world, c, r, piece);
                    if (world.HasPiece(c, r))
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    int c = piece.column, r = piece.row - i - 1;
                    Create(world, c, r, piece);
                    if (world.HasPiece(c, r))
                    {
                        break;
                    }
                }
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, int column, int row)
        {
            if (!base.IsValid(gameController, piece, column, row))
                return false;

            if (piece.opposed)
            {
                return row <= 8;
            }
            else
                return row >= 2;
        }
    }
}
