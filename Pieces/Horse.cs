namespace Board
{
    public class Horse : Move
    {
        public override void CreateMovable(IGameController controller, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row + i + 1;
                Create(controller, c, r, piece);
                if (HasPiece(controller, c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row - i - 1;
                Create(controller, c, r, piece);
                if (HasPiece(controller, c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row + i + 1;
                Create(controller, c, r, piece);
                if (HasPiece(controller, c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row - i - 1;
                Create(controller, c, r, piece);
                if (HasPiece(controller, c, r))
                {
                    break;
                }
            }
            Create(controller, piece.column + 1, piece.row, piece);
            Create(controller, piece.column, piece.row + 1, piece);
            Create(controller, piece.column - 1, piece.row, piece);
            Create(controller, piece.column, piece.row - 1, piece);
        }
    }
}
