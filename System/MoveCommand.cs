namespace Board
{
    public class MoveCommand : Command
    {
        Location location;

        public MoveCommand(World world, PieceModel piece, Location location) : base(world, piece)
        {
            this.location = location;
        }

        public override void Execute()
        {
            Piece.target = PieceModel.Position(location.Column, location.Row);
            Piece.Location = location;
            Piece.activated = true;
            World.CurrentPlayer = Piece.Player.Opposed();
        }

        public override string ToString()
        {
            return "{\"type\":\"move\",\"location\":" + location + "}";
        }
    }
}
