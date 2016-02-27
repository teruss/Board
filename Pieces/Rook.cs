namespace Board
{
    public class Rook : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            for (int i = 0; i < 8; i++)
            {
                int c = l.Column + i + 1, r = l.Row;
                var loc = new Location(r, c);
                world.CreateTransversableCell(loc, piece);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = l.Column, r = l.Row - i - 1;
                var loc = new Location(r, c);
                world.CreateTransversableCell(loc, piece);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = l.Column - i - 1, r = l.Row;
                var loc = new Location(r, c);
                world.CreateTransversableCell(loc, piece);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int c = l.Column, r = l.Row + i + 1;
                var loc = new Location(r, c);
                world.CreateTransversableCell(loc, piece);
                if (world.HasPiece(loc))
                {
                    break;
                }
            }
        }
    }
}
