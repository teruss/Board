namespace Board
{
    public class Lance : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var l = piece.Location;
            if (piece.opposed)
            {
                for (int i = 0; i < 8; i++)
                {
                    var loc = Location.Create(l.Column, l.Row + i + 1);
                    piece.CreateTraversableCell(world, loc);
                    if (world.HasPiece(loc))
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    var loc = Location.Create(l.Column, l.Row - i - 1);
                    piece.CreateTraversableCell(world, loc);
                    if (world.HasPiece(loc))
                    {
                        break;
                    }
                }
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, Location location)
        {
            if (!base.IsValid(gameController, piece, location))
                return false;

            if (piece.opposed)
            {
                return location.Row <= 8;
            }
            else
                return location.Row >= 2;
        }
    }
}
