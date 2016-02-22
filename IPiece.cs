using UnityEngine;

namespace Board
{
    public interface IPiece
    {
        Vector3 target { get; set; }
        bool activated { get; set; }
        Vector3 Position(float c, float r);
        int column { get; set; }
        int row { get; set; }
        bool promoted { get; set; }
        bool opposed { get; set; }
        bool captured { get; set; }
        void SetPromoted(bool p);
        void UpdateSprite();
        PieceType type { get; set; }
    }
}
