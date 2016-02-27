namespace Board
{
    public class SilverGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            if (piece.opposed)
                world.CreateTransversableCell(new Location(piece.row + 1, piece.column), piece);
            world.CreateTransversableCell(new Location(piece.row + 1, piece.column - 1), piece);
            world.CreateTransversableCell(new Location(piece.row + 1, piece.column + 1), piece);
            if (!piece.opposed)
                world.CreateTransversableCell(new Location(piece.row - 1, piece.column), piece);
            world.CreateTransversableCell(new Location(piece.row - 1, piece.column - 1), piece);
            world.CreateTransversableCell(new Location(piece.row - 1, piece.column + 1), piece);
        }
    }
}
