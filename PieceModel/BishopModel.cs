using System;
using System.Collections.Generic;
using System.Linq;

namespace Board
{
    public class BishopModel : PinnableModel
    {
        public BishopModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Bishop), Move.CreateInstancePromoted(PieceType.Bishop), location, PieceType.Bishop, player)
        {

        }

        internal override Direction CalcDirection(KingModel king)
        {
            if (king.Location.Column - king.Location.Row == Location.Column - Location.Row)
                return Direction.Slash;
            if (king.Location.Column + king.Location.Row == Location.Column + Location.Row)
                return Direction.BackSlash;
            return Direction.AnyWhere;
        }

        internal override bool IsBetween(Direction dir, KingModel king, PieceModel piece)
        {
            var l = piece.Location;
            if (dir == Direction.Slash)
                if (king.Location.Column - king.Location.Row == l.Column - l.Row)
                    return (king.Location.Row - l.Row) * (Location.Row - l.Row) < 0;
            if (dir == Direction.BackSlash)
                if (king.Location.Column + king.Location.Row == l.Column + l.Row)
                    return (king.Location.Row - l.Row) * (Location.Row - l.Row) < 0;
            return false;
        }
    }
}
