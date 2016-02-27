namespace Board
{
    public class Capture : Command
    {
        World world;

        public Capture(PieceModel piece, World world) : base(piece)
        {
            this.world = world;
        }

        public override void Execute(SpriteController spriteController)
        {
            piece.GetCaptured(world, spriteController);
        }

        public override void Undo(SpriteController spriteController)
        {
            world.GetKomadai(piece.opposed).Drop(piece);
            base.Undo(spriteController);
        }
    }
}
