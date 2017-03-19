namespace Board
{
    public class Capture : Command
    {
        World world;

        public Capture(PieceModel piece, World world) : base(world, piece)
        {
            this.world = world;
        }

        public override void Execute()
        {
            world.Capture(Piece);
        }

        public override void Undo()
        {
            world.GetKomadai(Piece.Player).Drop(Piece);
            base.Undo();
        }

        public override string ToString()
        {
            return "{\"type\":\"capture\"}";
        }
    }
}
