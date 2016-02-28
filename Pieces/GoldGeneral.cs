namespace Board
{
    public class GoldGeneral : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            piece.CreateTraversableCell(world, Location.Create(piece.Location.Column, piece.Location.Row + 1));
            if (piece.Player == Player.Black)
            {
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column - 1, piece.Location.Row + 1));
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column + 1, piece.Location.Row + 1));
            }
            piece.CreateTraversableCell(world, Location.Create(piece.Location.Column, piece.Location.Row - 1));
            if (piece.Player == Player.White)
            {
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column - 1, piece.Location.Row - 1));
                piece.CreateTraversableCell(world, Location.Create(piece.Location.Column + 1, piece.Location.Row - 1));
            }
            piece.CreateTraversableCell(world, Location.Create(piece.Location.Column - 1, piece.Location.Row));
            piece.CreateTraversableCell(world, Location.Create(piece.Location.Column + 1, piece.Location.Row));
        }
    }
}
