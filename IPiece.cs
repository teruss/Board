using UnityEngine;

namespace Board
{
    public interface IPiece
    {
        int column { get; set; }
        int row { get; set; }
        bool promoted { get; set; }
        bool opposed { get; set; }
        bool captured { get; set; }
        void SetPromoted(bool p);
        void UpdateSprite();
        PieceType type { get; set; }
        IMovableCell CreateCell(int column, int row);
        void Destroy();
    }
}
