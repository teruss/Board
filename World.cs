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
        Komadai komadai = new Komadai(false);
        Komadai opposedKomadai = new Komadai(true);
        public bool Alternate { get; private set; }

        public SpriteController SpriteController { get; set; }
        public IList<TraversableCell> TraversableCells { get; private set; }
        public MoveController MoveController { get; private set; }
        public Player CurrentPlayer { get; set; }
        public ChoiceDialog ChoiseDialog { get; private set; }
        public event EventHandler KingKilled, ChoiseDialogAppeared, ChoiceDialogDisappeared;

        public PieceManager PieceManager { get; private set; }

        public World() : this(false) { }
        public World(bool alternate)
        {
            TraversableCells = new List<TraversableCell>();
            MoveController = new MoveController();
            CurrentPlayer = Player.Gray;
            Alternate = alternate;
            PieceManager = new PieceManager();
        }

        public IList<PieceModel> Pieces()
        {
            return PieceManager.Pieces();
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
            PieceManager.Clear();
        }

        public void AddPiece(PieceModel piece)
        {
            PieceManager.Add(piece);
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
        public PieceModel GetPiece(Location location)
        {
            foreach (var p in Pieces())
            {
                if (!p.captured && p.Location == location)
                {
                    return p;
                }
            }
            return null;
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
                ChoiseDialog = new ChoiceDialog();
                if (ChoiseDialogAppeared != null)
                {
                    ChoiseDialogAppeared(this, EventArgs.Empty);
                }
                ChoiseDialog.Promoted.Executed += (sender, e) =>
                {
                    MoveAndPromote(cell);
                    ChoiseDialog = null;
                    OnChoiceDialogDisappeared();
                };
                ChoiseDialog.NotPromoted.Executed += (sender, e) =>
                {
                    Move(cell);
                    ChoiseDialog = null;
                    OnChoiceDialogDisappeared();
                };

                onPromotable();
            }
            else
            {
                if (cell.MustPromoted)
                    MoveAndPromote(cell);
                else
                    Move(cell);
            }
            DestroyTraversableCells();
        }

        void OnChoiceDialogDisappeared()
        {
            if (ChoiceDialogDisappeared != null)
            {
                ChoiceDialogDisappeared(this, EventArgs.Empty);
            }
        }

        public void DestroyTraversableCells()
        {
            foreach (var cell in TraversableCells)
            {
                cell.Destroy();
            }
            TraversableCells.Clear();
        }

        public void Capture(PieceModel piece)
        {
            piece.GetCaptured();

            GetKomadai(piece.Player).Accept(piece);
            if (piece.type == PieceType.King)
            {
                if (KingKilled != null)
                {
                    KingKilled(this, EventArgs.Empty);
                }
            }
        }
    }
}
