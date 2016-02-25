namespace Board
{
    public class Capture : Command
    {
        IGameController controller;

        public Capture(PieceModel piece, IGameController controller) : base(piece)
        {
            this.controller = controller;
        }

        public override void Execute()
        {
            piece.opposed = !piece.opposed;
            piece.captured = true;
            piece.row = piece.column = 0;
            piece.promoted = false;
            controller.GetKomadai(piece.opposed).Accept(piece);
            piece.UpdateSprite();
            piece.activated = true;
        }

        public override void Undo()
        {
            controller.GetKomadai(piece.opposed).Drop(piece);
            base.Undo();
        }
    }
}
