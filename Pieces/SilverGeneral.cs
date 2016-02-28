namespace Board
{
    public class SilverGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            if (piece.opposed)
                piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row + 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row + 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row + 1));
            if (!piece.opposed)
                piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row - 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row - 1));
            piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row - 1));
        }
    }
}
