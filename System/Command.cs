using UnityEngine;

namespace Board
{
    public abstract class Command
    {
        Location location;
        bool prevCaptured, prevPromoted, prevOpposed;
        Vector3 prevTarget;

        public PieceModel Piece { get; private set; }
        public abstract void Execute();
        public Command() { }

        public Command(PieceModel piece)
        {
            Piece = piece;
            location = piece.Location;
            prevCaptured = piece.captured;
            prevPromoted = piece.promoted;
            prevOpposed = piece.opposed;
            prevTarget = piece.target;
        }

        public virtual void Undo()
        {
            Piece.Location = location;
            Piece.target = prevTarget;
            Piece.opposed = prevOpposed;
            Piece.captured = prevCaptured;
            Piece.SetPromoted(prevPromoted);
            Piece.activated = true;
        }
    }
}
