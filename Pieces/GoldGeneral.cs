namespace Board
{
    public class GoldGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            world.Create(new Location(piece.row + 1, piece.column), piece);
            if (piece.opposed)
            {
                world.Create(new Location(piece.row + 1, piece.column - 1), piece);
                world.Create(new Location(piece.row + 1, piece.column + 1), piece);
            }
            world.Create(new Location(piece.row - 1, piece.column), piece);
            if (!piece.opposed)
            {
                world.Create(new Location(piece.row - 1, piece.column - 1), piece);
                world.Create(new Location(piece.row - 1, piece.column + 1), piece);
            }
            world.Create(new Location(piece.row, piece.column - 1), piece);
            world.Create(new Location(piece.row, piece.column + 1), piece);
        }
    }
}
