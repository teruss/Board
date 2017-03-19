namespace Board
{
    public class Promote : Command
    {
        public Promote(World world, PieceModel piece) : base(world, piece)
        {
        }

        public override void Execute()
        {
            Piece.SetPromoted(true);
        }

        public override string ToString()
        {
            return "{\"type\":\"promote\"}";
        }
    }
}
