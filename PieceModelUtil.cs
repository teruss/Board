namespace Board
{
    public class PieceModelUtil
    {
        static MoveDictionary moveDictionary = new MoveDictionary();

        public static PieceModel CreatePieceModel(Location location, PieceType type, Player player)
        {
            if (type == PieceType.King)
            {
                return new KingModel(location, player);
            }
            if (type == PieceType.Bishop)
            {
                return new BishopModel(location, player);
            }
            if (type == PieceType.Rook)
                return new RookModel(location, player);
            if (type == PieceType.Lance)
                return new LanceModel(location, player);
            return new PieceModel(moveDictionary.Get(type), moveDictionary.GetPromoted(type), location, type, player);
        }

        public static PieceModel CreatePieceModel(PieceType type)
        {
            return CreatePieceModel(new Location(), type, Player.Gray);
        }
    }
}
