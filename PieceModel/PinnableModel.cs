using System;

namespace Board
{
    public abstract class PinnableModel : PieceModel, IPinnableModel
    {
        public PinnableModel(Move move, Move promotedMove, Location location, PieceType type, Player player) : base(move, promotedMove, location, type, player)
        {
        }

        public abstract Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece);

        public bool IsFriendlyWith(PieceModel piece)
        {
            return Player == piece.Player;
        }
    }
}
