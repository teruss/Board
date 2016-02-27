using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class World
    {
        List<PieceModel> pieces = new List<PieceModel>();
        Komadai komadai = new Komadai(false);
        Komadai opposedKomadai = new Komadai(true);

        public SpriteController SpriteController { get; set; }
        public IList<TraversableCell> MovableCells { get; private set; }

        public World()
        {
            MovableCells = new List<TraversableCell>();
        }
        public IList<PieceModel> Pieces()
        {
            return pieces.AsReadOnly();
        }

        public void AddMovableCell(TraversableCell cell)
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

        public bool HasPiece(int c, int r)
        {
            foreach (var p in Pieces())
            {
                if (!p.captured && p.column == c && p.row == r)
                {
                    return true;
                }
            }
            return false;
        }

        public void Create(Location l, PieceModel piece)
        {
            if (l.Column < 1 || l.Column > 9 || l.Row < 1 || l.Row > 9)
                return;
            foreach (var p in Pieces())
            {
                if (!p.captured && p != piece && piece.opposed == p.opposed && l.Row == p.row && l.Column == p.column)
                    return;
            }
            var cell = GameObject.Instantiate(piece.Piece.movable, piece.UpperPosition(l.Column, l.Row), Quaternion.identity) as MovableCell;
            AddMovableCell(cell.Model);
            cell.Model.Set(l.Column, l.Row, piece);
        }
    }
}
