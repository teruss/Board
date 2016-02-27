namespace Board
{
    public struct Location
    {
        public int Row, Column;

        public Location(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Location Create(int file, int rank)
        {
            return new Location(rank, file);
        }

        public void Clear()
        {
            Row = Column = 0;
        }

        public static bool operator !=(Location lhs, Location rhs)
        {
            return lhs.Row != rhs.Row || lhs.Column != rhs.Column;
        }

        public static bool operator ==(Location lhs, Location rhs)
        {
            return lhs.Row == rhs.Row && lhs.Column == rhs.Column;
        }

        public override bool Equals(object obj)
        {
            return this == (Location)obj;
        }

        public override int GetHashCode()
        {
            return Row << 16 | Column;
        }
    }

}