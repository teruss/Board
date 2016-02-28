using UnityEngine.Assertions;

namespace Board
{
    public enum Player
    {
        White,
        Black,
        Gray
    }

    public static class Extensions
    {
        public static Player Opposed(this Player player)
        {
            if (player == Player.Black)
            {
                return Player.White;
            }
            else if (player == Player.White)
            {
                return Player.Black;
            }
            else
            {
                Assert.AreNotEqual(Player.Gray, player);
                return Player.Gray;
            }
        }
    }
}
