namespace Board
{
    public abstract class Move
    {
        public static Move CreateInstance(PieceType type)
        {
            if (type == PieceType.King)
                return new King();
            if (type == PieceType.Rook)
                return new Rook();
            if (type == PieceType.Bishop)
                return new Bishop();
            if (type == PieceType.GoldGeneral)
                return new GoldGeneral();
            if (type == PieceType.SilverGeneral)
                return new SilverGeneral();
            if (type == PieceType.Knight)
                return new Knight();
            if (type == PieceType.Lance)
                return new Lance();
            if (type == PieceType.Pawn)
                return new Pawn();
            return null;
        }

        public static Move CreateInstancePromoted(PieceType type)
        {
            if (type == PieceType.King)
                return new King();
            if (type == PieceType.Rook)
                return new Dragon();
            if (type == PieceType.Bishop)
                return new Horse();
            return new GoldGeneral();
        }

        public abstract void CreateMovable(IGameController controller, IPiece piece);

        public void Create(IGameController controller, int column, int row, IPiece piece)
        {
            if (column < 1 || column > 9 || row < 1 || row > 9)
                return;
            foreach (var p in controller.Pieces())
            {
                if (!p.captured && p != piece && piece.opposed == p.opposed && row == p.row && column == p.column)
                    return;
            }
            var cell = piece.CreateCell(column, row);
            cell.Set(column, row, piece);
        }

        protected bool HasPiece(IGameController controller, int c, int r)
        {
            foreach (var p in controller.Pieces())
            {
                if (!p.captured && p.column == c && p.row == r)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool IsValid(IGameController gameController, IPiece piece, int column, int row)
        {
            foreach (var p in gameController.Pieces())
            {
                if (p != piece && !p.captured && p.column == column && p.row == row)
                    return false;
            }
            return true;
        }
    }
}
