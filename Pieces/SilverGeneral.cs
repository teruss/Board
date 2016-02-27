namespace Board
{
    public class SilverGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            if (piece.opposed)
                world.Create(new Location(piece.row + 1, piece.column), piece);
            world.Create(new Location(piece.row + 1, piece.column - 1), piece);
            world.Create(new Location(piece.row + 1, piece.column + 1), piece);
            if (!piece.opposed)
                world.Create(new Location(piece.row - 1, piece.column), piece);
            world.Create(new Location(piece.row - 1, piece.column - 1), piece);
            world.Create(new Location(piece.row - 1, piece.column + 1), piece);
        }
    }
}
