namespace Board
{
    public class King : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            world.Create(piece.column, piece.row + 1, piece);
            world.Create(piece.column - 1, piece.row + 1, piece);
            world.Create(piece.column + 1, piece.row + 1, piece);
            world.Create(piece.column, piece.row - 1, piece);
            world.Create(piece.column - 1, piece.row - 1, piece);
            world.Create(piece.column + 1, piece.row - 1, piece);
            world.Create(piece.column - 1, piece.row, piece);
            world.Create(piece.column + 1, piece.row, piece);
        }
    }
}
