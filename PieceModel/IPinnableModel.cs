namespace Board
{
    public interface IPinnableModel
    {
        Direction GetDirection(KingModel king, PieceModel piece);
    }
}
