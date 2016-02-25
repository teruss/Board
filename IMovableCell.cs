namespace Board
{
    public interface IMovableCell
    {
        void Set(int column, int row, PieceModel piece);
        void Destroy();
    }
}
