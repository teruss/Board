namespace Board
{
    public class Knight : Move
    {
        public override void CreateMovable(World controller, PieceModel piece)
        {
            if (!piece.opposed)
            {
                Create(controller, piece.column + 1, piece.row - 2, piece);
                Create(controller, piece.column - 1, piece.row - 2, piece);
            }
            else
            {
                Create(controller, piece.column + 1, piece.row + 2, piece);
                Create(controller, piece.column - 1, piece.row + 2, piece);
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, int column, int row)
        {
            if (!base.IsValid(gameController, piece, column, row))
                return false;

            if (piece.opposed)
            {
                return row <= 7;
            }
            else
                return row >= 3;
        }
    }
}
