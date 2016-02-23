namespace Board
{
    public class Rook : Move
    {
        public override void CreateMovable(IGameController controller, IPiece piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column + i + 1, r = piece.row;
                Create(controller, c, r, piece);
                if (HasPiece(controller, c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column, r = piece.row - i - 1;
                Create(controller, c, r, piece);
                if (HasPiece(controller, c, r))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.column - i - 1, r = piece.row;
                Create(controller, c, r, piece);
                if (HasPiece(controller, c, r))
                {
                    break;
                }
            }
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
    }
}
