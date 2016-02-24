namespace Board
{
    public interface IMovableCell
    {
        void Set(int column, int row, IPiece piece);
        void Destroy();
    }
}
