namespace Board
{
    public class ChoiceDialog
    {
        public ChoicePiece Promoted { get; private set; }
        public ChoicePiece NotPromoted { get; private set; }

        public ChoiceDialog()
        {
            Promoted = new ChoicePiece();
            NotPromoted = new ChoicePiece();
        }
    }
}
