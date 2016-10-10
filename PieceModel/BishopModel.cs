namespace Board
{
    public class BishopModel : PieceModel
    {
        public BishopModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Bishop), Move.CreateInstancePromoted(PieceType.Bishop), location, PieceType.Bishop, player)
        {

        }

        public override bool CanCheckAfterMove(World world, Location l, PieceModel piece)
        {
            if (Location.Column - Location.Row != l.Column - l.Row)
            {
                if (Check(world, piece, (int i) => { return new Location(Location.Row + 1 + i, Location.Column + 1 + i); }))
                {
                    return true;
                }
                if (Check(world, piece, (int i) => { return new Location(Location.Row - 1 - i, Location.Column - 1 - i); }))
                {
                    return true;
                }
            }
            if (Location.Column + Location.Row != l.Column + l.Row)
            {
                if (Check(world, piece, (int i) => { return new Location(Location.Row + 1 + i, Location.Column - 1 - i); }))
                {
                    return true;
                }
                if (Check(world, piece, (int i) => { return new Location(Location.Row - 1 - i, Location.Column + 1 + i); }))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
