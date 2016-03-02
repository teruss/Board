using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Board
{
    public class PieceModel
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
            if (GameController.alternate && world.CurrentPlayer != Player && world.CurrentPlayer != Player.Gray)
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
                if (!p.captured && p != this && Player == p.Player && l == p.Location)
                    return;
            }
            var t = new TraversableCell(this, l);
            world.AddMovableCell(t);

            if (OnCreateTraversableCell != null)
                OnCreateTraversableCell(this, new TraversalCellEventArgs() { TraversableCell = t });
        }

        public void GetCaptured(World world)
        {
            Assert.AreNotEqual(Player.Gray, Player);
            Player =  Player.Opposed();
            captured = true;
            Location.Clear();
            promoted = false;
            world.GetKomadai(Player).Accept(this);
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
    }
}
