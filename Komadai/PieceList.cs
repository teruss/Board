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
            pieces.Add(piece);
        }

        public void Remove(PieceModel piece)
        {
            pieces.Remove(piece);
        }
    }
}
