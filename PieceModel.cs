using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Board
{
    public class PieceModel : IComparable
    {
        public const float rowSize = 0.64f, columnSize = 0.6f;

        Move move, promotedMove;
        public event EventHandler OnUpdateSprite;
        public event EventHandler OnDestroy;
        public event EventHandler OnCreateTraversableCell;

        public Location Location { get; set; }
        public bool captured { get; set; }
        public bool promoted { get; set; }
        bool _activated;
        public bool sleep { get; set; }
        public Player Player { get; set; }
        public bool activated
        {
            get { return _activated; }
            set
            {
                _activated = value;
                sleep = true;
            }
        }
        public Vector3 target { get; set; }
        public PieceType type { get; set; }

        public PieceModel(Move move, Move promotedMove, Location location, PieceType type, Player player)
        {
            this.move = move;
            this.promotedMove = promotedMove;
            Location = location;
            this.type = type;
            Player = player;

            target = Position(location.Column, location.Row);
        }

        public static Vector3 Position(float c, float r)
        {
            return new Vector3((5 - c) * columnSize, (5 - r) * rowSize);
        }

        public static Vector3 Position(Vector2 v)
        {
            return Position(v.X, v.Y);
        }

        public void SetPromoted(bool p)
        {
            promoted = p;
            UpdateSprite();
        }

        public void Destroy()
        {
            if (OnDestroy != null)
                OnDestroy(this, EventArgs.Empty);
        }

        public static Vector3 UpperPosition(float c, float r)
        {
            return new Vector3((5 - c) * columnSize, (5 - r) * rowSize, -1);
        }

        public void CreateMovable(World world)
        {
            if (promoted)
            {
                promotedMove.CreateMovable(world, this);
            }
            else
            {
                move.CreateMovable(world, this);
            }
        }

        public void Drop(World world)
        {
            for (int r = 1; r <= 9; r++)
            {
                for (int c = 1; c <= 9; c++)
                {
                    var l = Location.Create(c, r);
                    if (move.IsValid(world, this, l))
                        CreateTraversableCell(world, l);
                }
            }
        }

        public void DropOrCreateMovable(World world)
        {
            if (world.Alternate && world.CurrentPlayer != Player && world.CurrentPlayer != Player.Gray)
                return;

            if (captured)
            {
                Drop(world);
                return;
            }

            CreateMovable(world);
        }

        public void CreateTraversableCell(World world, Location l)
        {
            if (l.Column < 1 || l.Column > 9 || l.Row < 1 || l.Row > 9)
                return;
            foreach (var p in world.Pieces())
            {
                if (IsFriendly(l, p))
                    return;
                if (IsPinnedBy(world, l, p))
                    return;
            }
            var t = new TraversableCell(this, l);
            world.AddMovableCell(t);

            if (OnCreateTraversableCell != null)
                OnCreateTraversableCell(this, new TraversalCellEventArgs() { TraversableCell = t });
        }

        private bool IsFriendly(Location l, PieceModel p)
        {
            return !p.captured && p != this && Player == p.Player && l == p.Location;
        }

        private bool IsPinnedBy(World world, Location l, PieceModel piece)
        {
            if (piece.type == PieceType.Rook && piece.Player != Player)
            {
                if (piece.CanCheckAfterMove(world, l, this))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanCheckAfterMove(World world, Location l, PieceModel piece)
        {
            if (type == PieceType.Rook)
            {
                if (Location.Row != l.Row)
                {
                    if (Check(world, piece, (int i) => { return new Location(Location.Row, Location.Column + 1 + i); }))
                    {
                        return true;
                    }
                    if (Check(world, piece, (int i) => { return new Location(Location.Row, Location.Column - 1 - i); }))
                    {
                        return true;
                    }
                }

                if (Location.Column != l.Column)
                {
                    if (Check(world, piece, (int i) => { return new Location(Location.Row + 1 + i, Location.Column); }))
                    {
                        return true;
                    }

                    if (Check(world, piece, (int i) => { return new Location(Location.Row - 1 - i, Location.Column); }))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool Check(World world, PieceModel piece, Func<int, Location> f)
        {
            int countBetweenKing = 0;
            bool foundKing = false;
            for (int i = 0; i < 8; i++)
            {
                var location = f(i);
                if (location.Row < 0 || location.Column<0 || location.Row > 9 || location.Column > 9)
                {
                    return false;
                }
                var x = world.GetPiece(location);
                if (x == null)
                {
                    continue;
                }
                if (x.type == PieceType.King)
                {
                    foundKing = true;
                    break;
                }
                if (x != piece)
                {
                    return false;
                }
                countBetweenKing++;
            }
            return foundKing && countBetweenKing == 1;
        }

        public void GetCaptured()
        {
            Assert.AreNotEqual(Player.Gray, Player);
            Player = Player.Opposed();
            captured = true;
            Location.Clear();
            promoted = false;
            activated = true;
            UpdateSprite();
        }

        void UpdateSprite()
        {
            if (OnUpdateSprite != null)
                OnUpdateSprite(this, EventArgs.Empty);
        }

        public void GetDropped(World world)
        {
            captured = false;
            promoted = false;
            world.GetKomadai(Player).Drop(this);
            activated = true;
            UpdateSprite();
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            var otherPieceModel = obj as PieceModel;
            if (otherPieceModel != null)
                return type.CompareTo(otherPieceModel.type);
            else
                throw new ArgumentException("Object is not a PieceModel");
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }
}
