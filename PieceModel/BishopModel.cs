namespace Board
{
    public class BishopModel : PinnableModel
    {
        public BishopModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Bishop), Move.CreateInstancePromoted(PieceType.Bishop), location, PieceType.Bishop, player)
        {

        }

        public override Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            var kingLocation = king.Location;
            var pieceLocation = piece.Location;
            var bishopLocation = Location;
            var diff = bishopLocation.Column - bishopLocation.Row;

            if (kingLocation.Column - kingLocation.Row == diff && pieceLocation.Column - pieceLocation.Row == diff)
            {
                if ((kingLocation.Column - pieceLocation.Column) * (bishopLocation.Column - pieceLocation.Column) >= 0)
                {
                    return Direction.AnyWhere;
                }

                foreach (var p in manager.Pieces())
                {
                    if (p == king || p == piece)
                        continue;
                    var l = p.Location;
                    if (l.Column - l.Row == diff)
                    {
                        if ((kingLocation.Row - l.Row) * (Location.Row - l.Row) < 0)
                        {
                            return Direction.AnyWhere;
                        }
                    }
                }

                return Direction.Slash;
            }

            var sum = bishopLocation.Column + bishopLocation.Row;
            if (kingLocation.Column + kingLocation.Row == sum && pieceLocation.Column + pieceLocation.Row == sum)
            {
                if ((kingLocation.Column - pieceLocation.Column) * (bishopLocation.Column - pieceLocation.Column) >= 0)
                {
                    return Direction.AnyWhere;
                }

                foreach (var p in manager.Pieces())
                {
                    if (p == king || p == piece)
                        continue;
                    var l = p.Location;
                    if (l.Column + l.Row == sum)
                    {
                        if ((Location.Column - l.Column) * (kingLocation.Column - l.Column) < 0)
                        {
                            return Direction.AnyWhere;
                        }
                    }
                }

                return Direction.BackSlash;
            }

            return Direction.AnyWhere;
        }
    }
}
