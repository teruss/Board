﻿using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public abstract class Command
    {
        public Location PrevLocation;
        bool prevCaptured, prevPromoted;
        Player prevPlayer;
        Vector3 prevTarget;
        Player prevWorldPlayer;

        public World World { get; private set; }

        public PieceModel Piece;
        public abstract void Execute();
        public Command() { }

        public Command(World world, PieceModel piece)
        {
            this.World = world;
            prevWorldPlayer = world.CurrentPlayer;
            Piece = piece;
            PrevLocation = piece.Location;
            prevCaptured = piece.captured;
            prevPromoted = piece.promoted;
            prevPlayer = piece.Player;
            prevTarget = piece.target;
        }

        public virtual void Undo()
        {
            World.CurrentPlayer = prevWorldPlayer;
            Piece.Location = PrevLocation;
            Piece.target = prevTarget;
            Piece.Player = prevPlayer;
            Piece.captured = prevCaptured;
            Piece.SetPromoted(prevPromoted);
            Piece.activated = true;
        }
    }
}
