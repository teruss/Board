using System;
using UnityEngine;

namespace Board
{
    public class PieceModel
    {
        public const float rowSize = 0.64f, columnSize = 0.6f;

        Move move, promotedMove;
        public event EventHandler OnUpdateSprite;
        public event EventHandler OnDestroy;
        public event EventHandler OnCreateTransversableCell;

        public int column { get; set; }
        public int row { get; set; }
        public bool opposed { get; set; }
        public bool captured { get; set; }
        public bool promoted { get; set; }
        bool _activated;
        public bool sleep { get; set; }
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

        public PieceModel(Move move, Move promotedMove, int column, int row, PieceType type, bool opposed)
        {
            this.move = move;
            this.promotedMove = promotedMove;
            this.column = column;
            this.row = row;
            this.type = type;
            this.opposed = opposed;

            target = Position(column, row);
        }

        public Vector3 Position(float c, float r)
        {
            return new Vector3((5 - c) * columnSize, (5 - r) * rowSize);
        }

        public void SetPromoted(SpriteController spriteController, bool p)
        {
            promoted = p;
            UpdateSprite(spriteController);
        }

        public void UpdateSprite(SpriteController spriteController)
        {
            if (OnUpdateSprite != null)
                OnUpdateSprite(this, EventArgs.Empty);
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
                    if (move.IsValid(world, this, c, r))
                        world.CreateTransversableCell(new Location(r, c), this);
                }
            }
        }

        public void DropOrCreateMovable(World world)
        {
            if (captured)
            {
                Drop(world);
                return;
            }

            CreateMovable(world);
        }

        public void CreateTraversableCell(Location l)
        {
            if (OnCreateTransversableCell != null)
                OnCreateTransversableCell(this, new LocationEventArgs() { Location = l });
        }
    }
}
