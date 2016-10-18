using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Board
{
    public class PieceManager
    {
        public KingModel WhiteKing, BlackKing;

        List<PieceModel> pieces = new List<PieceModel>();
        List<IPinnableModel> pinnables = new List<IPinnableModel>();

        internal void Add(PieceModel piece)
        {
            pieces.Add(piece);

            if (piece.Player == Player.Black)
            {
                if (piece.type == PieceType.King)
                {
                    BlackKing = piece as KingModel;
                }
            }
            else if (piece.Player == Player.White)
            {
                if (piece.type == PieceType.King)
                {
                    WhiteKing = piece as KingModel;
                }
            }

            if (piece is IPinnableModel)
            {
                pinnables.Add(piece as IPinnableModel);
            }
        }

        internal Direction GetPinnedDirection(PieceModel piece)
        {
            var king = GetFriendlyKing(piece);
            foreach (var pinnable in pinnables)
            {
                if (pinnable.IsFriendlyWith(piece))
                    continue;
                var d = pinnable.GetDirection(this, king, piece);
                if (d != Direction.AnyWhere)
                {
                    return d;
                }
            }
            return Direction.AnyWhere;
        }

        internal KingModel GetFriendlyKing(PieceModel piece)
        {
            return piece.Player == Player.Black ? BlackKing : WhiteKing;
        }

        internal KingModel GetEnemyKing(PieceModel piece)
        {
            return piece.Player == Player.Black ? WhiteKing : BlackKing;
        }

        public IList<PieceModel> Pieces()
        {
            return pieces.AsReadOnly();
        }

        internal void Clear()
        {
            pieces.Clear();
        }

        internal IEnumerable<PieceModel> GetPiecesOnBoard()
        {
            return pieces.Where(x => !x.captured);
        }
    }
}
