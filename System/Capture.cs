﻿namespace Board
{
    public class Capture : Command
    {
        World controller;

        public Capture(PieceModel piece, World controller) : base(piece)
        {
            this.controller = controller;
        }

        public override void Execute(SpriteController spriteController)
        {
            piece.opposed = !piece.opposed;
            piece.captured = true;
            piece.row = piece.column = 0;
            piece.promoted = false;
            controller.GetKomadai(piece.opposed).Accept(piece);
            piece.UpdateSprite(spriteController);
            piece.activated = true;
        }

        public override void Undo(SpriteController spriteController)
        {
            controller.GetKomadai(piece.opposed).Drop(piece);
            base.Undo(spriteController);
        }
    }
}
