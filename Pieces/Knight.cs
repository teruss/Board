namespace Board
{
    public class Knight : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            if (!piece.opposed)
            {
                world.CreateTransversableCell(new Location(l.Row - 2, l.Column + 1), piece);
                world.CreateTransversableCell(new Location(l.Row - 2, l.Column - 1), piece);
            }
            else
            {
                world.CreateTransversableCell(new Location(l.Row + 2, l.Column + 1), piece);
                world.CreateTransversableCell(new Location(l.Row + 2, l.Column - 1), piece);
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, Location location)
        {
            if (!base.IsValid(gameController, piece, location))
                return false;

            if (piece.opposed)
            {
                return location.Row <= 7;
            }
            else
                return location.Row >= 3;
        }
    }
}
