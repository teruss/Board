namespace Board
{
    public class KingModel : PieceModel
    {
        public KingModel(Location location, Player player) : base(Move.CreateInstance(PieceType.King), Move.CreateInstancePromoted(PieceType.King), location, PieceType.King, player)
        {

        }
    }
}
