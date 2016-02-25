﻿using UnityEngine;

namespace Board
{
    public abstract class Command
    {
        protected PieceModel piece;

        int prevFile, prevRank;
        bool prevCaptured, prevPromoted, prevOpposed;
        Vector3 prevTarget;

        public abstract void Execute(SpriteController spriteController);

        public Command()
        {
        }

        public Command(PieceModel piece)
        {
            this.piece = piece;
            prevFile = piece.column;
            prevRank = piece.row;
            prevCaptured = piece.captured;
            prevPromoted = piece.promoted;
            prevOpposed = piece.opposed;
            prevTarget = piece.target;
        }

        public virtual void Undo(SpriteController spriteController)
        {
            piece.row = prevRank;
            piece.column = prevFile;
            piece.target = prevTarget;
            piece.opposed = prevOpposed;
            piece.captured = prevCaptured;
            piece.SetPromoted(spriteController, prevPromoted);
            piece.activated = true;
        }
    }
}
