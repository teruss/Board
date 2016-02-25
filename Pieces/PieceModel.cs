namespace Board
{
    public class PieceModel
    {
        public int column { get; set; }
        public int row { get; set; }
        public PieceType type { get; set; }
        public bool opposed { get; set; }

        public PieceModel(int column, int row, PieceType type, bool opposed)
        {
            this.column = column;
            this.row = row;
            this.type = type;
            this.opposed = opposed;
        }
    }
}
