using UnityEngine;

namespace Board
{
    public abstract class Command
    {
        protected PieceModel piece;

        Location location;
        bool prevCaptured, prevPromoted, prevOpposed;
        Vector3 prevTarget;

        public abstract void Execute();

        public Command()
        {
        }

        public Command(PieceModel piece)
        {
            this.piece = piece;
            location = piece.Location;
            prevCaptured = piece.captured;
            prevPromoted = piece.promoted;
            prevOpposed = piece.opposed;
            prevTarget = piece.target;
        }

        public virtual void Undo()
        {
            piece.Location = location;
            piece.target = prevTarget;
            piece.opposed = prevOpposed;
            piece.captured = prevCaptured;
            piece.SetPromoted(prevPromoted);
            piece.activated = true;
        }
    }
}
