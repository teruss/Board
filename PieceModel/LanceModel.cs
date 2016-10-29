using System;
using UnityEngine.Assertions;

namespace Board
{
    public class LanceModel : PinnableModel
    {
        public LanceModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Lance), Move.CreateInstancePromoted(PieceType.Lance), location, PieceType.Lance, player)
        {

        }

        internal override Direction CalcDirection(KingModel king)
        {
            if (promoted || king.Location.Column != Location.Column)
                return Direction.AnyWhere;

            if (king.Player == Player.White && Location.Row < king.Location.Row)
                return Direction.Down;
            if (king.Player == Player.Black && Location.Row > king.Location.Row)
                return Direction.Up;

            return Direction.AnyWhere;
        }

        internal override bool IsBetween(Direction dir, KingModel king, PieceModel piece)
        {
            if (piece.Location.Column != Location.Column)
                return false;
            if (dir == Direction.Down)
                return Location.Row < piece.Location.Row && piece.Location.Row < king.Location.Row;
            if (dir == Direction.Up)
                return Location.Row > piece.Location.Row && piece.Location.Row > king.Location.Row;
            return false;
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
