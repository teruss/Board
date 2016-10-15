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
                        return Direction.AnyWhere;
                    }
                }
                if (e.Row == l.Row && Location.Row == l.Row)
                {
                    if ((Location.Column - l.Column) * (e.Column - l.Column) < 0)
                    {
                        return Direction.AnyWhere;
                    }
                }
            }

            var d = piece.Location;

            if (e.Column == d.Column && Location.Column == d.Column)
            {
                if ((Location.Row - d.Row) * (e.Row - d.Row) < 0)
                {
                    return Direction.Vertical;
                }
            }
            if (e.Row == d.Row && Location.Row == d.Row)
            {
                if ((Location.Column - d.Column) * (e.Column - d.Column) < 0)
                {
                    return Direction.Horizontal;
                }
            }
            return Direction.AnyWhere;
        }
    }
}
