namespace Board
{
    public class MoveCommand : Command
    {
        Location location;

        public MoveCommand(PieceModel piece, Location location) : base(piece)
        {
            this.piece = piece;
            this.location = location;
        }

        public override void Execute()
        {
            piece.target = PieceModel.Position(location.Column, location.Row);
            piece.Location = location;
            piece.activated = true;
        }
    }
}
