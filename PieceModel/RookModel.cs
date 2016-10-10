namespace Board
{
    public class RookModel : PieceModel
    {
        public RookModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Rook), Move.CreateInstancePromoted(PieceType.Rook), location, PieceType.Rook, player)
        {

        }

        public override bool CanCheckAfterMove(World world, Location l, PieceModel piece)
        {
            if (Location.Row != l.Row)
            {
                if (Check(world, piece, (int i) => { return new Location(Location.Row, Location.Column + 1 + i); }))
                {
                    return true;
                }
                if (Check(world, piece, (int i) => { return new Location(Location.Row, Location.Column - 1 - i); }))
                {
                    return true;
                }
            }

            if (Location.Column != l.Column)
            {
                if (Check(world, piece, (int i) => { return new Location(Location.Row + 1 + i, Location.Column); }))
                {
                    return true;
                }

                if (Check(world, piece, (int i) => { return new Location(Location.Row - 1 - i, Location.Column); }))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
