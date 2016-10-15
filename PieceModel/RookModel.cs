namespace Board
{
    public class RookModel : PieceModel, IPinnableModel
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

        public Direction GetDirection(KingModel king, PieceModel piece)
        {
            var e = king.Location;
            var s = Location;
            var d = piece.Location;
            if (e.Column == d.Column && s.Column == d.Column)
            {
                //if (e.Row < s.Row)
                //{
                //    return Direction.Up;
                //}
                if (e.Row > s.Row)
                {
                    return Direction.Down;
                }
            }
            //var sum = s.Column + s.Row;
            //if (e.Column + e.Row == sum && d.Column + d.Row == sum)
            //{
            //    if (e.Column < s.Column)
            //    {
            //        return Direction.DownRight;
            //    }
            //    if (e.Column > s.Column)
            //    {
            //        return Direction.UpLeft;
            //    }
            //}
            return Direction.None;
        }
    }
}
