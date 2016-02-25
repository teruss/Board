namespace Board
{
    public class MoveCommand : Command
    {
        int file, rank;

        public MoveCommand(PieceModel piece, int file, int rank) : base(piece)
        {
            this.piece = piece;
            this.file = file;
            this.rank = rank;
        }

        public override void Execute(SpriteController spriteController)
        {
            piece.target = piece.Position(file, rank);
            piece.row = rank;
            piece.column = file;
            piece.activated = true;
        }
    }
}
