namespace Board
{
    public class MoveCommand : Command
    {
        Location location;

        public MoveCommand(PieceModel piece, Location location) : base(piece)
        {
            this.location = location;
        }

        public override void Execute()
        {
            Piece.target = PieceModel.Position(location.Column, location.Row);
            Piece.Location = location;
            Piece.activated = true;
        }
    }
}
