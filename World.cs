using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

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
        public IList<TraversableCell> TraversableCells { get; private set; }
        public MoveController MoveController { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public World()
        {
            TraversableCells = new List<TraversableCell>();
            MoveController = new MoveController();
            CurrentPlayer = Player.Gray;
        }
        public IList<PieceModel> Pieces()
        {
            return pieces.AsReadOnly();
        }

        public void AddMovableCell(TraversableCell cell)
        {
            TraversableCells.Add(cell);
        }
        public Komadai GetKomadai(Player player)
        {
            Assert.AreNotEqual(Player.Gray, player);
            if (player == Player.Black)
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

        public PieceModel CreatePieceModel(Location location, PieceType type, Player player)
        {
            return new PieceModel(moveDictionary.Get(type), moveDictionary.GetPromoted(type), location, type, player);
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
            }
            else {
                if (cell.MustPromoted)
                    MoveAndPromote(cell);
                else
                    Move(cell);
            }
            DestroyTraversableCells();
        }

        public void DestroyTraversableCells()
        {
            foreach (var cell in TraversableCells)
            {
                cell.Destroy();
            }
            TraversableCells.Clear();
        }
    }
}
