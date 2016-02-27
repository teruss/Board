namespace Board
{
    public class King : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            world.CreateTransversableCell(new Location(piece.Location.Row + 1, piece.Location.Column), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row + 1, piece.Location.Column - 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row + 1, piece.Location.Column + 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row - 1, piece.Location.Column), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row - 1, piece.Location.Column - 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row - 1, piece.Location.Column + 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row, piece.Location.Column - 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row, piece.Location.Column + 1), piece);
        }
    }
}
