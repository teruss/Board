using System;
using UnityEngine.Assertions;

namespace Board
{
    public class LanceModel : PieceModel
    {
        public LanceModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Lance), Move.CreateInstancePromoted(PieceType.Lance), location, PieceType.Lance, player)
        {

        }

        public override bool CanCheckAfterMove(World world, Location l, PieceModel piece)
        {
            var enemyKing = world.PieceManager.GetEnemyKing(this);
            if (enemyKing.Location.Column != Location.Column)
            {
                return false;
            }
            if (!IsOnTheLine(enemyKing))
            {
                return false;
            }
            if (Location.Column == l.Column)
            {
                return false;
            }

            var piecesBetween = world.PieceManager.GetPiecesBetween(this, enemyKing);
            if (piecesBetween.Count != 1)
            {
                return false;
            }
            if (piecesBetween[0] != piece)
            {
                return false;
            }
            return true;
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
