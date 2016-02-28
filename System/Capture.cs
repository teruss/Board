namespace Board
{
    public class Capture : Command
    {
        World world;

        public Capture(PieceModel piece, World world) : base(piece)
        {
            this.world = world;
        }

        public override void Execute()
        {
            Piece.GetCaptured(world);
        }

        public override void Undo()
        {
            world.GetKomadai(Piece.Player).Drop(Piece);
            base.Undo();
        }
    }
}
