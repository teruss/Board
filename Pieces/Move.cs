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

        public abstract void CreateMovable(World controller, PieceModel piece);

        public virtual bool IsValid(World gameController, PieceModel piece, Location location)
        {
            foreach (var p in gameController.Pieces())
            {
                if (p != piece && !p.captured && p.Location == location)
                    return false;
            }
            return true;
        }

        public abstract bool IsPinnable
        {
            get;
        }
    }
}
