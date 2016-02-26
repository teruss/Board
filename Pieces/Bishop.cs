namespace Board
{
    public class Bishop : Move
    {
        public override void CreateMovable(World controller, PieceModel piece)
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
        }
    }
}
