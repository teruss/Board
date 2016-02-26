﻿namespace Board
{
    public class GoldGeneral : Move
    {
        public override void CreateMovable(World controller, PieceModel piece)
        {
            Create(controller, piece.column, piece.row + 1, piece);
            if (piece.opposed)
            {
                Create(controller, piece.column - 1, piece.row + 1, piece);
                Create(controller, piece.column + 1, piece.row + 1, piece);
            }
            Create(controller, piece.column, piece.row - 1, piece);
            if (!piece.opposed)
            {
                Create(controller, piece.column - 1, piece.row - 1, piece);
                Create(controller, piece.column + 1, piece.row - 1, piece);
            }
            Create(controller, piece.column - 1, piece.row, piece);
            Create(controller, piece.column + 1, piece.row, piece);
        }
    }
}
