using System.Collections.Generic;
using System.Linq;

namespace Board
{
    public abstract class PinnableModel : PieceModel, IPinnableModel
    {
        public PinnableModel(Move move, Move promotedMove, Location location, PieceType type, Player player) : base(move, promotedMove, location, type, player)
        {
        }

        public abstract Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece);
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
