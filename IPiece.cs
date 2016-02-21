using UnityEngine;

namespace Board
{
    public interface IPiece
    {
        Vector3 target { get; set; }
        bool activated { get; set; }
        Vector3 Position(float c, float r);
    }
}
