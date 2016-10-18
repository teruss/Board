using System;

namespace Board
{
    public class RookModel : PinnableModel
    {
        public RookModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Rook), Move.CreateInstancePromoted(PieceType.Rook), location, PieceType.Rook, player)
        {

        }

        public override Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            var dir = CalcDirection(king, piece);
            if (dir == Direction.AnyWhere)
                return dir;

            foreach (var p in manager.GetPiecesOnBoard())
            {
                if (p == king || p == piece)
                    continue;
                if (IsBetween(dir, king, p))
                    return Direction.AnyWhere;
            }

            return dir;
        }

        private bool IsBetween(Direction dir, KingModel king, PieceModel p)
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

        private Direction CalcDirection(KingModel king, PieceModel piece)
        {
            if (king.Location.Column == piece.Location.Column && Location.Column == piece.Location.Column)
                if ((king.Location.Row - piece.Location.Row) * (Location.Row - piece.Location.Row) < 0)
                    return Direction.Vertical;
            if (king.Location.Row == piece.Location.Row && Location.Row == piece.Location.Row)
                if ((Location.Column - piece.Location.Column) * (king.Location.Column - piece.Location.Column) < 0)
                    return Direction.Horizontal;
            return Direction.AnyWhere;
        }
    }
}
