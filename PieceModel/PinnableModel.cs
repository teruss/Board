using System.Collections.Generic;
using System.Linq;

namespace Board
{
    public abstract class PinnableModel : PieceModel, IPinnableModel
    {
        public PinnableModel(Move move, Move promotedMove, Location location, PieceType type, Player player) : base(move, promotedMove, location, type, player)
        {
        }

        public virtual Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            var dir = CalcDirection(king);
            if (dir == Direction.AnyWhere)
                return dir;
            if (!IsBetween(dir, king, piece))
                return Direction.AnyWhere;

            foreach (var p in manager.GetPiecesOnBoard())
            {
                if (p == king || p == piece)
                    continue;
                if (IsBetween(dir, king, p))
                    return Direction.AnyWhere;
            }

            return dir;
        }

        internal abstract bool IsBetween(Direction dir, KingModel king, PieceModel piece);
        internal abstract Direction CalcDirection(KingModel king);

        public IEnumerable<PieceModel> GetPiecesOnBoard(PieceManager manager)
        {
            return manager.Pieces().Where(x => !x.captured);
        }

        public bool IsFriendlyWith(PieceModel piece)
        {
            return Player == piece.Player;
        }
    }
}
