namespace Board
{
    public class SilverGeneral : Move
    {
        public override void CreateMovable(IGameController controller, IPiece piece)
        {
            if (piece.opposed)
                Create(controller, piece.column, piece.row + 1, piece);
            Create(controller, piece.column - 1, piece.row + 1, piece);
            Create(controller, piece.column + 1, piece.row + 1, piece);
            if (!piece.opposed)
                Create(controller, piece.column, piece.row - 1, piece);
            Create(controller, piece.column - 1, piece.row - 1, piece);
            Create(controller, piece.column + 1, piece.row - 1, piece);
        }
    }
}
