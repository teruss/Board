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
        public bool activated { get { return piece.activated; } set { piece.activated = value; } }
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

        public void CreateMovable(IGameController controller)
        {
            if (promoted)
            {
                promotedMove.CreateMovable(controller, this);
            }
            else
            {
                move.CreateMovable(controller, this);
            }
        }

        public bool IsValid(IGameController controller, int row, int column)
        {
            return move.IsValid(controller, this, column, row);
        }

        public void Create(IGameController controller, int row, int column)
        {
            move.Create(controller, column, row, this);
        }
        public void Drop(IGameController controller)
        {
            for (int r = 1; r <= 9; r++)
            {
                for (int c = 1; c <= 9; c++)
                {
                    if (IsValid(controller, r, c))
                        Create(controller, r, c);
                }
            }
        }

        public void DestroyAndCreateMovable(IGameController controller)
        {
            controller.DestroyMovableCells();

            CreateMovable(controller);
        }

        public void DropOrCreateMovable(IGameController controller)
        {
            if (captured)
            {
                Drop(controller);
                return;
            }

            DestroyAndCreateMovable(controller);
        }
    }
}
