namespace Board
{
    public class RookModel : PieceModel
    {
        public RookModel(Location location, Player player) : base(Move.CreateInstance(PieceType.Rook), Move.CreateInstancePromoted(PieceType.Rook), location, PieceType.Rook, player)
        {

        }

        public override bool CanCheckAfterMove(World world, Location l, PieceModel piece)
        {
            var enemyKing = world.PieceManager.GetEnemyKing(this);
            var piecesBetween = world.PieceManager.GetPiecesBetween(this, piece, l, enemyKing);
            return piecesBetween.Count == 0;
        }
    }
}
