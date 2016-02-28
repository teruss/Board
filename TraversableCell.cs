using System;

namespace Board
{
    public class TraversableCell
    {
        public event EventHandler OnDestroy;

        public PieceModel Piece { get; private set; }
        public Location Location { get; private set; }

        public TraversableCell(Location location)
        {
            Location = location;
        }
        public bool IsPromotable
        {
            get
            {
                if (Piece.captured || Piece.promoted || Piece.type == PieceType.King || Piece.type == PieceType.GoldGeneral)
                    return false;
                if (Piece.type == PieceType.Knight)
                {
                    if (Piece.opposed)
                        return Location.Row == 7;
                    else
                        return Location.Row == 3;
                }
                if (Piece.type == PieceType.Pawn || Piece.type == PieceType.Lance)
                {
                    if (Piece.opposed)
                        return Location.Row == 7 || Location.Row == 8;
                    else
                        return Location.Row == 3 || Location.Row == 2;
                }
                if (Piece.opposed)
                {
                    return Location.Row >= 7 || Piece.Location.Row >= 7;
                }
                else
                {
                    return Location.Row <= 3 || Piece.Location.Row <= 3;
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
                        return Location.Row >= 8;
                    else
                        return Location.Row <= 2;
                }
                if (Piece.type == PieceType.Pawn || Piece.type == PieceType.Lance)
                {
                    if (Piece.opposed)
                        return Location.Row == 9;
                    else
                        return Location.Row == 1;
                }
                return false;
            }
        }
        public void Set(PieceModel piece)
        {
            Piece = piece;
        }
        public void Destroy()
        {
            if (OnDestroy != null)
                OnDestroy(this, EventArgs.Empty);
        }
    }
}
