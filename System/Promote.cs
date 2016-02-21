namespace Board
{
    public class Promote : Command
    {
        public Promote(IPiece piece) : base(piece)
        {
        }

        public override void Execute()
        {
            piece.SetPromoted(true);
        }
    }
}
