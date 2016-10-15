using UnityEngine.Assertions;

namespace Board
{
    public class LanceModel : PieceModel, IPinnableModel
    {
        public LanceModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Lance), Move.CreateInstancePromoted(PieceType.Lance), location, PieceType.Lance, player)
        {

        }

        public Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            if (promoted)
                return Direction.None;
            return Direction.Up;
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
