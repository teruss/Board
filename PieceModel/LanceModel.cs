using UnityEngine.Assertions;

namespace Board
{
    public class LanceModel : PinnableModel
    {
        public LanceModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Lance), Move.CreateInstancePromoted(PieceType.Lance), location, PieceType.Lance, player)
        {

        }

        public override Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            if (promoted)
                return Direction.AnyWhere;
            var e = king.Location;
            if (e.Column == piece.Location.Column && e.Column == Location.Column)
            {
                foreach (var p in manager.Pieces())
                {
                    if (p == king || p == piece)
                        continue;
                    var l = p.Location;
                    if (e.Column == l.Column && Location.Column == l.Column)
                    {
                        if ((e.Row - l.Row) * (Location.Row - l.Row) < 0)
                        {
                            return Direction.AnyWhere;
                        }
                    }
                    if (e.Row == l.Row && Location.Row == l.Row)
                    {
                        if ((Location.Column - l.Column) * (e.Column - l.Column) < 0)
                        {
                            return Direction.AnyWhere;
                        }
                    }
                }

                if (piece.Player == Player.White)
                    if (piece.Location.Row < king.Location.Row)
                        return Direction.Vertical;
                if (piece.Player == Player.Black)
                    if (piece.Location.Row > king.Location.Row)
                        return Direction.Vertical;
            }
            return Direction.AnyWhere;
        }

        private bool IsOnTheLine(KingModel enemyKing)
        {
            if (Player == Player.White)
                return enemyKing.Location.Row <= Location.Row - 2;
            if (Player == Player.Black)
                return enemyKing.Location.Row >= Location.Row + 2;

            Assert.AreNotEqual(Player.Gray, Player);
            return false;
        }
    }
}
