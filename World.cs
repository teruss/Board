using System.Collections.Generic;

namespace Board
{
    public class World
    {
        List<PieceModel> pieces = new List<PieceModel>();
        Komadai komadai = new Komadai(false);
        Komadai opposedKomadai = new Komadai(true);

        public SpriteController SpriteController { get; set; }
        public IList<IMovableCell> MovableCells { get; private set; }

        public World()
        {
            MovableCells = new List<IMovableCell>();
        }
        public IList<PieceModel> Pieces()
        {
            return pieces.AsReadOnly();
        }

        public void AddMovableCell(IMovableCell cell)
        {
            MovableCells.Add(cell);
        }
        public Komadai GetKomadai(bool opposed)
        {
            if (opposed)
                return opposedKomadai;
            return komadai;
        }

        public void ClearPieces()
        {
            foreach (var p in Pieces())
            {
                p.Destroy();
            }
            pieces.Clear();
        }

        public void AddPiece(PieceModel piece)
        {
            pieces.Add(piece);
        }
    }
}
