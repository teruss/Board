namespace Board
{
    public class Promote : Command
    {
        public Promote(PieceModel piece) : base(piece)
        {
        }

        public override void Execute(SpriteController spriteController)
        {
            piece.SetPromoted(spriteController, true);
        }
    }
}
