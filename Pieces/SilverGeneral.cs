namespace Board
{
    public class SilverGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            if (piece.opposed)
                world.CreateTransversableCell(new Location(l.Row + 1, l.Column), piece);
            world.CreateTransversableCell(new Location(l.Row + 1, l.Column - 1), piece);
            world.CreateTransversableCell(new Location(l.Row + 1, l.Column + 1), piece);
            if (!piece.opposed)
                world.CreateTransversableCell(new Location(l.Row - 1, l.Column), piece);
            world.CreateTransversableCell(new Location(l.Row - 1, l.Column - 1), piece);
            world.CreateTransversableCell(new Location(l.Row - 1, l.Column + 1), piece);
        }
    }
}
