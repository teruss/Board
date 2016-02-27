namespace Board
{
    public class SilverGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            if (piece.opposed)
                world.Create(piece.column, piece.row + 1, piece);
            world.Create(piece.column - 1, piece.row + 1, piece);
            world.Create(piece.column + 1, piece.row + 1, piece);
            if (!piece.opposed)
                world.Create(piece.column, piece.row - 1, piece);
            world.Create(piece.column - 1, piece.row - 1, piece);
            world.Create(piece.column + 1, piece.row - 1, piece);
        }
    }
}
