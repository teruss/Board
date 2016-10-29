using System;

namespace Board
{
    public class RookModel : PinnableModel
    {
        public RookModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Rook), Move.CreateInstancePromoted(PieceType.Rook), location, PieceType.Rook, player)
        {

        }

        internal override bool IsBetween(Direction dir, KingModel king, PieceModel p)
        {
            var l = p.Location;
            if (dir == Direction.Vertical)
                if (king.Location.Column == l.Column && Location.Column == l.Column)
                    return (king.Location.Row - l.Row) * (Location.Row - l.Row) < 0;
            if (dir == Direction.Horizontal)
                if (king.Location.Row == l.Row && Location.Row == l.Row)
                    return (Location.Column - l.Column) * (king.Location.Column - l.Column) < 0;
            return false;
        }


        internal override Direction CalcDirection(KingModel king)
        {
            if (king.Location.Column == Location.Column)
                return Direction.Vertical;
            if (king.Location.Row == Location.Row)
                return Direction.Horizontal;
            return Direction.AnyWhere;
        }
    }
}
