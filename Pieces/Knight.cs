namespace Board
{
    public class Knight : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            if (!piece.opposed)
            {
                piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row - 2));
                piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row - 2));
            }
            else
            {
                piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row + 2));
                piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row + 2));
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
