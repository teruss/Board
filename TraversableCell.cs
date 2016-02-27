using System;

namespace Board
{
    public class TraversableCell
    {
        public event EventHandler OnDestroy;

        public PieceModel Piece { get; private set; }

        public int Column { get; private set; }
        public int Row { get; private set; }
        public bool IsPromotable
        {
            get
            {
                if (Piece.captured || Piece.promoted || Piece.type == PieceType.King || Piece.type == PieceType.GoldGeneral)
                    return false;
                if (Piece.type == PieceType.Knight)
                {
                    if (Piece.opposed)
                        return Row == 7;
                    else
                        return Row == 3;
                }
                if (Piece.type == PieceType.Pawn || Piece.type == PieceType.Lance)
                {
                    if (Piece.opposed)
                        return Row == 7 || Row == 8;
                    else
                        return Row == 3 || Row == 2;
                }
                if (Piece.opposed)
                {
                    return Row >= 7 || Piece.row >= 7;
                }
                else
                {
                    return Row <= 3 || Piece.row <= 3;
                }
            }
        }

        public bool MustPromoted
        {
            get
            {
                if (Piece.type == PieceType.Knight)
                {
                    if (Piece.opposed)
                        return Row >= 8;
                    else
                        return Row <= 2;
                }
                if (Piece.type == PieceType.Pawn || Piece.type == PieceType.Lance)
                {
                    if (Piece.opposed)
                        return Row == 9;
                    else
                        return Row == 1;
                }
                return false;
            }
        }
        public void Set(int column, int row, PieceModel piece)
        {
            Column = column;
            Row = row;
            Piece = piece;
        }
        public void Destroy()
        {
            if (OnDestroy != null)
                OnDestroy(this, EventArgs.Empty);
        }
    }
}
