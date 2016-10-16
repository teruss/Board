namespace Board
{
    public interface IPinnableModel
    {
        Direction GetDirection(PieceManager manager, KingModel king, PieceModel piece);
        bool IsFriendlyWith(PieceModel piece);
    }
}
