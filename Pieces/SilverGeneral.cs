namespace Board
{
    public class SilverGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            var d = world.PieceManager.GetPinnedDirection(piece);
            var l = piece.Location;

            if ((d & Direction.Vertical) != 0)
            {
                if (piece.Player == Player.Black)
                    piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row + 1));
                if (piece.Player == Player.White)
                    piece.CreateTraversableCell(world, Location.Create(l.Column, l.Row - 1));
            }

            if ((d & Direction.Slash) != 0)
            {
                piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row + 1));
                piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row - 1));
            }

            if ((d & Direction.BackSlash) != 0)
            {
                piece.CreateTraversableCell(world, Location.Create(l.Column - 1, l.Row + 1));
                piece.CreateTraversableCell(world, Location.Create(l.Column + 1, l.Row - 1));
            }
        }
    }
}
