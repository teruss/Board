namespace Board
{
    public class Knight : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            if (!piece.opposed)
            {
                world.Create(piece.column + 1, piece.row - 2, piece);
                world.Create(piece.column - 1, piece.row - 2, piece);
            }
            else
            {
                world.Create(piece.column + 1, piece.row + 2, piece);
                world.Create(piece.column - 1, piece.row + 2, piece);
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
