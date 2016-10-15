namespace Board
{
    public class BishopModel : PieceModel
    {
        public BishopModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Bishop), Move.CreateInstancePromoted(PieceType.Bishop), location, PieceType.Bishop, player)
        {

        }

        public override bool CanCheckAfterMove(World world, Location l, PieceModel piece)
        {
            var enemyKing = world.PieceManager.GetEnemyKing(this);
            return world.PieceManager.GetPiecesBetween(this, piece, l, enemyKing);
        }
    }
}
