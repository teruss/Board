using System;
using System.Collections.Generic;

namespace Board
{
    public class TraversalCellEventArgs : EventArgs
    {
        public TraversableCell TraversableCell { get; set; }
    }
    public class World
    {
        List<PieceModel> pieces = new List<PieceModel>();
        Komadai komadai = new Komadai(false);
        Komadai opposedKomadai = new Komadai(true);
        MoveDictionary moveDictionary = new MoveDictionary();

        public SpriteController SpriteController { get; set; }
        public IList<TraversableCell> MovableCells { get; private set; }
        public MoveController MoveController { get; private set; }

        public World()
        {
            MovableCells = new List<TraversableCell>();
            MoveController = new MoveController();
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

        public PieceModel CreatePieceModel(Location location, PieceType type, bool opposed)
        {
            return new PieceModel(moveDictionary.Get(type), moveDictionary.GetPromoted(type), location, type, opposed);
        }

        public void MoveAndPromote(TraversableCell cell)
        {
            MoveController.MoveAndPromote(this, cell.Piece, cell.Location);
        }

        public void Move(TraversableCell cell)
        {
            MoveController.Move(this, cell.Piece, cell.Location);
        }

        public void Select(TraversableCell cell, Action onPromotable)
        {
            if (cell.IsPromotable)
            {
                onPromotable();
                return;
            }
            if (cell.MustPromoted)
                MoveAndPromote(cell);
            else
                Move(cell);
        }
    }
}
