namespace Board
{
    public class Pawn : Move
    {
        public override void CreateMovable(World world, PieceModel piece)
        {
            if (piece.opposed)
            {
                world.Create(piece.column, piece.row + 1, piece);
            }
            else
            {
                world.Create(piece.column, piece.row - 1, piece);
            }
        }

        public override bool IsValid(World gameController, PieceModel piece, int file, int rank)
        {
            if (!base.IsValid(gameController, piece, file, rank))
                return false;

            foreach (var p in gameController.Pieces())
            {
                if (p != piece && !p.captured && !p.promoted && p.type == PieceType.Pawn && piece.opposed == p.opposed && p.column == file)
                    return false;
            }

            if (piece.opposed)
            {
                return rank <= 8;
            }
            else
                return rank >= 2;
        }
    }
}
