namespace Board
{
    public class Bishop : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column + i + 1, r = piece.Location.Row + i + 1;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column + i + 1, r = piece.Location.Row - i - 1;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column - i - 1, r = piece.Location.Row + i + 1;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column - i - 1, r = piece.Location.Row - i - 1;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
        }
    }
}
