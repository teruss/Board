using System;

namespace Board
{
    [Flags]
    public enum Direction
    {
        None = 0,
        Right = 1 << 1,
        Left = 1 << 2,
        Up = 1 << 3,
        Down = 1 << 4,
        UpRight = 1 << 5,
        UpLeft = 1 << 6,
        DownRight = 1 << 7,
        DownLeft = 1 << 8
    }
}
