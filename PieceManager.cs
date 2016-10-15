using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Board
{
    public class PieceManager
    {
        public KingModel WhiteKing, BlackKing;

        List<PieceModel> pieces = new List<PieceModel>();
        List<PieceModel> pinnables = new List<PieceModel>();

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

            if (piece.HasPinnableMove)
            {
                pinnables.Add(piece);
            }
        }

        internal Direction GetPinnedDirection(PieceModel piece)
        {
            var king = GetFriendlyKing(piece);
            var res = Direction.None;
            foreach (var pinnable in pinnables)
            {
                var d = GetDirection(king, piece, pinnable);
                res |= d;
            }
            return res;
        }

        private Direction GetDirection(KingModel king, PieceModel piece, PieceModel pinnable)
        {
            var e = king.Location;
            var s = pinnable.Location;
            var d = piece.Location;
            var diff = s.Column - s.Row;
            if (e.Column - e.Row == diff && d.Column - d.Row == diff)
            {
                if (e.Column < s.Column)
                {
                    return Direction.UpRight;
                }
                if (e.Column > s.Column)
                {
                    return Direction.DownLeft;
                }
            }
            var sum = s.Column + s.Row;
            if (e.Column + e.Row == sum && d.Column + d.Row == sum)
            {
                if (e.Column < s.Column)
                {
                    return Direction.DownRight;
                }
                if (e.Column > s.Column)
                {
                    return Direction.UpLeft;
                }
            }
            return Direction.None;
        }

        [Flags]
        internal enum Direction
        {
            None = 0,
            Right = 1 << 1,
            Left = 1 << 2,
            Up = 1 << 3,
            Down = 1 << 4,
            UpRight = 1 << 5,
            UpLeft = 1 << 6,
            DownRight = 1 << 7,
            DownLeft = 1 << 8
        }

        internal bool GetPiecesBetween(BishopModel bishopModel, PieceModel piece, Location target, KingModel enemyKing)
        {
            var e = enemyKing.Location;
            var s = bishopModel.Location;
            var d = GetDirection(e, s);

            var list = new List<PieceModel>();

            foreach (var p in pieces)
            {
                if (p != enemyKing && GetDirection(p.Location, s) == d)
                {
                    list.Add(p);
                }
            }
            if (list.Count == 1 && list[0] == piece)
            {
                if (GetDirection(target, s) != d)
                {
                    return true;
                }
            }
            return false;
        }

        private static Direction GetDirection(Location e, Location s)
        {
            if (e.Column - e.Row == s.Column - s.Row)
            {
                if (e.Column < s.Column)
                {
                    return Direction.UpRight;
                }
                if (e.Column > s.Column)
                {
                    return Direction.DownLeft;
                }
            }
            if (e.Column + e.Row == s.Column + s.Row)
            {
                if (e.Column < s.Column)
                {
                    return Direction.DownRight;
                }
                if (e.Column > s.Column)
                {
                    return Direction.UpLeft;
                }
            }
            return Direction.None;
        }

        internal KingModel GetFriendlyKing(PieceModel piece)
        {
            return piece.Player == Player.Black ? BlackKing : WhiteKing;
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
            foreach (var p in pieces)
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
