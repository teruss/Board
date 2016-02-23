namespace Board
{
    public class Lance : Move
    {
        public override void CreateMovable(IGameController controller, IPiece piece)
        {
            if (piece.opposed)
            {
                for (int i = 0; i < 8; i++)
                {
                    int c = piece.column, r = piece.row + i + 1;
                    Create(controller, c, r, piece);
                    if (HasPiece(controller, c, r))
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
                    Create(controller, c, r, piece);
                    if (HasPiece(controller, c, r))
                    {
                        break;
                    }
                }
            }
        }

        public override bool IsValid(IGameController gameController, IPiece piece, int column, int row)
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
