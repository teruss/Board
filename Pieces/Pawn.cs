namespace Board
{
    public class Pawn : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            if (piece.opposed)
            {
                world.CreateTransversableCell(new Location(l.Row + 1, l.Column), piece);
            }
            else
            {
                world.CreateTransversableCell(new Location(l.Row - 1, l.Column), piece);
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, Location location)
        {
            if (!base.IsValid(gameController, piece, location))
                return false;

            foreach (var p in gameController.Pieces())
            {
                if (p != piece && !p.captured && !p.promoted && p.type == PieceType.Pawn && piece.opposed == p.opposed && p.Location.Column == location.Column)
                    return false;
            }

            if (piece.opposed)
            {
                return location.Row <= 8;
            }
            else
                return location.Row >= 2;
        }
    }
}
