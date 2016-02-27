using System;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class LocationEventArgs : EventArgs
    {
        public Location Location { get; set; }
    }
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

        public bool HasPiece(Location location)
        {
            foreach (var p in Pieces())
            {
                if (!p.captured && p.Location == location)
                {
                    return true;
                }
            }
            return false;
        }

        public void CreateTransversableCell(Location l, PieceModel piece)
        {
            if (l.Column < 1 || l.Column > 9 || l.Row < 1 || l.Row > 9)
                return;
            foreach (var p in Pieces())
            {
                if (!p.captured && p != piece && piece.opposed == p.opposed && l == p.Location)
                    return;
            }
            piece.CreateTraversableCell(l);
        }
    }
}
