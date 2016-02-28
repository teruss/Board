namespace Board
{
    public class Drop : Command
    {
        World world;

        public Drop(PieceModel piece, World world) : base(piece)
        {
            this.world = world;
        }

        public override void Execute()
        {
            Piece.GetDropped(world);
        }

        public override void Undo()
        {
            world.GetKomadai(Piece.opposed).Accept(Piece);
            base.Undo();
        }
    }
}
