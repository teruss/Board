namespace Board
{
    public class ChoiceDialog
    {
        public ChoicePiece Promoted { get; private set; }
        public ChoicePiece NotPromoted { get; private set; }

        public ChoiceDialog(World world, TraversableCell cell)
        {
            Promoted = new ChoicePiece(world, cell, true);
            NotPromoted = new ChoicePiece(world, cell, false);
        }
    }
}
