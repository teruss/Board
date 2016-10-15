namespace Board
{
    public class BishopModel : PieceModel, IPinnableModel
    {
        public BishopModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Bishop), Move.CreateInstancePromoted(PieceType.Bishop), location, PieceType.Bishop, player)
        {

        }

        public Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            var e = king.Location;
            var s = Location;
            var d = piece.Location;
            var diff = s.Column - s.Row;
            if (e.Column - e.Row == diff && d.Column - d.Row == diff)
            {
                if (e.Column < s.Column)
                {
                    return Direction.UpRight;
                }
                if (e.Column > s.Column)
                {
                    return Direction.DownLeft;
                }
            }
            var sum = s.Column + s.Row;
            if (e.Column + e.Row == sum && d.Column + d.Row == sum)
            {
                if (e.Column < s.Column)
                {
                    return Direction.DownRight;
                }
                if (e.Column > s.Column)
                {
                    return Direction.UpLeft;
                }
            }
            return Direction.None;
        }
    }
}
