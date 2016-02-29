using UnityEngine;

namespace Board
{
    public abstract class Command
    {
        Location location;
        bool prevCaptured, prevPromoted;
        Player prevPlayer;
        Vector3 prevTarget;
        Player prevWorldPlayer;

        public World World { get; private set; }

        public PieceModel Piece { get; private set; }
        public abstract void Execute();
        public Command() { }

        public Command(World world, PieceModel piece)
        {
            this.World = world;
            prevWorldPlayer = world.CurrentPlayer;
            Piece = piece;
            location = piece.Location;
            prevCaptured = piece.captured;
            prevPromoted = piece.promoted;
            prevPlayer = piece.Player;
            prevTarget = piece.target;
        }

        public virtual void Undo()
        {
            World.CurrentPlayer = prevWorldPlayer;
            Piece.Location = location;
            Piece.target = prevTarget;
            Piece.Player = prevPlayer;
            Piece.captured = prevCaptured;
            Piece.SetPromoted(prevPromoted);
            Piece.activated = true;
        }
    }
}
