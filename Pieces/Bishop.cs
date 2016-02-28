namespace Board
{
    public class Bishop : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            for (int i = 0; i < 8; i++)
            {
                var l = Location.Create(piece.Location.Column + i + 1, piece.Location.Row + i + 1);
                piece.CreateTraversableCell(world, l);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                var l = Location.Create(piece.Location.Column + i + 1, piece.Location.Row - i - 1);
                piece.CreateTraversableCell(world, l);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                var l = Location.Create(piece.Location.Column - i - 1, piece.Location.Row + i + 1);
                piece.CreateTraversableCell(world, l);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                var l = Location.Create(piece.Location.Column - i - 1, piece.Location.Row - i - 1);
                piece.CreateTraversableCell(world, l);
                if (world.HasPiece(l))
                {
                    break;
                }
            }
        }
    }
}
