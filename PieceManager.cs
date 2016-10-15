using System.Collections.Generic;

namespace Board
{
    public class PieceManager
    {
        public KingModel WhiteKing, BlackKing;

        List<PieceModel> pieces = new List<PieceModel>();

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
        }

        internal KingModel GetEnemyKing(PieceModel piece)
        {
            return piece.Player == Player.Black ? WhiteKing : BlackKing;
        }

        internal IList<PieceModel> GetPiecesBetween(RookModel rookModel, PieceModel piece, Location target, KingModel enemyKing)
        {
            var list = new List<PieceModel>();
            foreach (var p in pieces)
            {
                var l = p == piece ? target : p.Location;

                if (l.Column == rookModel.Location.Column)
                {
                    if (enemyKing.Location.Row < l.Row && l.Row < rookModel.Location.Row)
                    {
                        list.Add(p);
                    }
                    if (rookModel.Location.Row < l.Row && l.Row < enemyKing.Location.Row)
                    {
                        list.Add(p);
                    }
                }

                if (l.Row == rookModel.Location.Row)
                {
                    if (enemyKing.Location.Column < l.Column && l.Column < rookModel.Location.Column)
                    {
                        list.Add(p);
                    }
                    if (rookModel.Location.Column < l.Column && l.Column < enemyKing.Location.Column)
                    {
                        list.Add(p);
                    }
                }
            }
            return list;
        }

        public IList<PieceModel> Pieces()
        {
            return pieces.AsReadOnly();
        }

        internal IList<PieceModel> GetPiecesBetween(LanceModel lanceModel, KingModel enemyKing)
        {
            var list = new List<PieceModel>();
            foreach(var p in pieces)
            {
                if (p.Location.Column == lanceModel.Location.Column)
                {
                    if (enemyKing.Location.Row < p.Location.Row && p.Location.Row < lanceModel.Location.Row)
                    {
                        list.Add(p);
                    }
                    if (lanceModel.Location.Row < p.Location.Row && p.Location.Row < enemyKing.Location.Row)
                    {
                        list.Add(p);
                    }
                }
            }
            return list;
        }

        internal void Clear()
        {
            pieces.Clear();
        }
    }
}
