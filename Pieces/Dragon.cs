namespace Board
{
    public class Dragon : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column + i + 1, r = piece.Location.Row;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column, r = piece.Location.Row - i - 1;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column - i - 1, r = piece.Location.Row;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = piece.Location.Column, r = piece.Location.Row + i + 1;
                var l = new Location(r, c);
                world.CreateTransversableCell(l, piece);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            world.CreateTransversableCell(new Location(piece.Location.Row + 1, piece.Location.Column + 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row - 1, piece.Location.Column + 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row + 1, piece.Location.Column - 1), piece);
            world.CreateTransversableCell(new Location(piece.Location.Row - 1, piece.Location.Column - 1), piece);
        }
    }
}
