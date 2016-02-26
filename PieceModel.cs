using UnityEngine;

namespace Board
{
    public class PieceModel
    {
        public const float rowSize = 0.64f, columnSize = 0.6f;

        Piece piece;
        Move move, promotedMove;

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
        public Vector3 target { get { return piece.target; } set { piece.target = value; } }
        public PieceType type { get { return piece.type; } set { piece.type = value; } }

        public PieceModel(Piece piece, SpriteController spriteController, Move move, Move promotedMove, int column, int row, PieceType type, bool opposed)
        {
            this.move = move;
            this.promotedMove = promotedMove;
            this.piece = piece;
            this.column = column;
            this.row = row;
            this.type = type;
            this.opposed = opposed;

            piece.spriteRenderer.sprite = spriteController.Get(type, promoted, opposed);
            piece.transform.parent = piece.board.transform;
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
            piece.spriteRenderer.sprite = spriteController.Get(type, promoted, opposed);
        }

        public void Destroy()
        {
            piece.Destroy();
        }

        public IMovableCell CreateCell(int column, int row)
        {
            return GameObject.Instantiate(piece.movable, UpperPosition(column, row), Quaternion.identity) as MovableCell;
        }

        public Vector3 UpperPosition(float c, float r)
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

        public bool IsValid(World world, int row, int column)
        {
            return move.IsValid(world, this, column, row);
        }

        public void Create(World world, int row, int column)
        {
            move.Create(world, column, row, this);
        }
        public void Drop(World world)
        {
            for (int r = 1; r <= 9; r++)
            {
                for (int c = 1; c <= 9; c++)
                {
                    if (IsValid(world, r, c))
                        Create(world, r, c);
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
    }
}
