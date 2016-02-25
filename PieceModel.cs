using UnityEngine;

namespace Board
{
    public class PieceModel
    {
        public const float rowSize = 0.64f, columnSize = 0.6f;

        Piece piece;

        public int column { get; set; }
        public int row { get; set; }
        public bool opposed { get; set; }
        public bool captured { get; set; }
        public bool promoted { get; set; }
        public bool activated { get { return piece.activated; } set { piece.activated = value; } }
        public Vector3 target { get { return piece.target; } set { piece.target = value; } }
        public PieceType type { get { return piece.type; } set { piece.type = value; } }

        public PieceModel(Piece piece, int column, int row, PieceType type, bool opposed)
        {
            this.piece = piece;
            this.column = column;
            this.row = row;
            this.type = type;
            this.opposed = opposed;

            piece.spriteRenderer.sprite = piece.spriteController.Get(type, promoted, opposed);
            piece.transform.parent = piece.board.transform;
            target = Position(column, row);
        }

        public Vector3 Position(float c, float r)
        {
            return new Vector3((5 - c) * columnSize, (5 - r) * rowSize);
        }

        public void SetPromoted(bool p)
        {
            promoted = p;
            UpdateSprite();
        }

        public void UpdateSprite()
        {
            piece.spriteRenderer.sprite = piece.spriteController.Get(type, promoted, opposed);
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
            var p = piece.spriteController.GetPiece(type);
            if (captured)
            {
                p.Drop(controller, this);
                return;
            }

            p.CreateMovable(controller, this);
        }

    }
}
