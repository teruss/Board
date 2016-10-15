namespace Board
{
    public class RookModel : PieceModel, IPinnableModel
    {
        public RookModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Rook), Move.CreateInstancePromoted(PieceType.Rook), location, PieceType.Rook, player)
        {

        }

        public Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            var e = king.Location;

            foreach (var p in manager.Pieces())
            {
                if (p == king || p == piece)
                    continue;
                var l = p.Location;
                if (e.Column == l.Column && Location.Column == l.Column)
                {
                    if ((e.Row - l.Row) * (Location.Row - l.Row) < 0)
                    {
                        return Direction.None;
                    }
                }
                if (e.Row == l.Row && Location.Row == l.Row)
                {
                    if ((Location.Column - l.Column) * (e.Column - l.Column) < 0)
                    {
                        return Direction.None;
                    }
                }
            }

            var d = piece.Location;

            if (e.Column == d.Column && Location.Column == d.Column)
            {
                if (e.Row < Location.Row)
                {
                    return Direction.Up;
                }
                if (e.Row > Location.Row)
                {
                    return Direction.Down;
                }
            }
            if (e.Row == d.Row && Location.Row == d.Row)
            {
                if ((Location.Column - d.Column) * (e.Column - d.Column) < 0)
                {
                    return Direction.Left | Direction.Right;
                }
            }
            return Direction.None;
        }
    }
}
