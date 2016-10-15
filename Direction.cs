using System;

namespace Board
{
    [Flags]
    public enum Direction
    {
        Right = 1 << 1,
        Left = 1 << 2,
        Horizontal = Right | Left,
        Up = 1 << 3,
        Down = 1 << 4,
        Vertical = Up | Down,
        UpRight = 1 << 5,
        DownLeft = 1 << 6,
        Slash = UpRight | DownLeft,
        UpLeft = 1 << 7,
        DownRight = 1 << 8,
        BackSlash = UpLeft | DownRight,
        AnyWhere = (1 << 9) - 1
    }
}
