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
            piece.GetCaptured(world);
        }

        public override void Undo()
        {
            world.GetKomadai(piece.opposed).Drop(piece);
            base.Undo();
        }
    }
}
