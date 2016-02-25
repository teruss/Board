namespace Board
{
    public class King : Move
    {
        public override void CreateMovable(IGameController controller, PieceModel piece)
        {
            Create(controller, piece.column, piece.row + 1, piece);
            Create(controller, piece.column - 1, piece.row + 1, piece);
            Create(controller, piece.column + 1, piece.row + 1, piece);
            Create(controller, piece.column, piece.row - 1, piece);
            Create(controller, piece.column - 1, piece.row - 1, piece);
            Create(controller, piece.column + 1, piece.row - 1, piece);
            Create(controller, piece.column - 1, piece.row, piece);
            Create(controller, piece.column + 1, piece.row, piece);
        }
    }
}
