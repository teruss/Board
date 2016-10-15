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

        public Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece)
        {
            var e = king.Location;

            foreach (var p in manager.Pieces())
            {
                if (p == king || p == piece)
                    continue;
                var l = p.Location;
                if (e.Column == l.Column && Location.Column == l.Column)
                {
                    if ((e.Row - l.Row) * (Location.Row - l.Row) < 0)
                    {
                        return Direction.None;
                    }
                }
                if (e.Row == l.Row && Location.Row == l.Row)
                {
                    if ((Location.Column - l.Column) * (e.Column - l.Column) < 0)
                    {
                        return Direction.None;
                    }
                }
            }

            var d = piece.Location;

            if (e.Column == d.Column && Location.Column == d.Column)
            {
                if (e.Row < Location.Row)
                {
                    return Direction.Up;
                }
                if (e.Row > Location.Row)
                {
                    return Direction.Down;
                }
            }
            if (e.Row == d.Row && Location.Row == d.Row)
            {
                //if (e.Column < s.Column)
                //{
                //    return Direction.DownRight;
                //}
                if (Location.Column < d.Column && d.Column < e.Column )
                {
                    return Direction.Left;
                }
            }
            return Direction.None;
        }
    }
}
