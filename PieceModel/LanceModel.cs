namespace Board
{
    public class LanceModel : PieceModel
    {
        public LanceModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Lance), Move.CreateInstancePromoted(PieceType.Lance), location, PieceType.Lance, player)
        {

        }

        public override bool CanCheckAfterMove(World world, Location l, PieceModel piece)
        {
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
