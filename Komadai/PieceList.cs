using System.Collections.Generic;

namespace Board
{
    public class PieceList
    {
        List<PieceModel> pieces = new List<PieceModel>();

        public int Count { get { return pieces.Count; } }

        public PieceModel this[int index]
        {
            get
            {
                return pieces[index];
            }
        }

        public void Add(PieceModel piece)
        {
            var index = pieces.BinarySearch(piece);
            if (index < 0)
                pieces.Insert(~index, piece);
            else
                pieces.Insert(index, piece);
        }

        public void Remove(PieceModel piece)
        {
            pieces.Remove(piece);
        }
    }
}
