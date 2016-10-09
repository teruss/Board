namespace Board
{
    public class PieceModelUtil
    {
        static MoveDictionary moveDictionary = new MoveDictionary();

        public static PieceModel CreatePieceModel(Location location, PieceType type, Player player)
        {
            return new PieceModel(moveDictionary.Get(type), moveDictionary.GetPromoted(type), location, type, player);
        }

        public static PieceModel CreatePieceModel(PieceType type)
        {
            return CreatePieceModel(new Location(), type, Player.Gray);
        }
    }
}
