namespace Board
{
    public class Drop : Command
    {
        IGameController controller;

        public Drop(PieceModel piece, IGameController controller) : base(piece)
        {
            this.controller = controller;
        }

        public override void Execute()
        {
            piece.captured = false;
            piece.promoted = false;
            controller.GetKomadai(piece.opposed).Drop(piece);
            piece.UpdateSprite();
            piece.activated = true;
        }

        public override void Undo()
        {
            controller.GetKomadai(piece.opposed).Accept(piece);
            base.Undo();
        }
    }
}
