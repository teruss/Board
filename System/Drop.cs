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
            piece.GetDropped(world);
        }

        public override void Undo()
        {
            world.GetKomadai(piece.opposed).Accept(piece);
            base.Undo();
        }
    }
}
