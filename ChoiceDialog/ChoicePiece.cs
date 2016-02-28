using System;

namespace Board
{
    public class ChoicePiece
    {
        public event EventHandler Executed;

        World world;
        TraversableCell cell;
        bool promoted;

        public ChoicePiece(World world, TraversableCell cell, bool promoted)
        {
            this.world = world;
            this.cell = cell;
            this.promoted = promoted;
        }
        public void Execute()
        {
            if (promoted)
                world.MoveAndPromote(cell);
            else
                world.Move(cell);
            if (Executed != null)
                Executed(this, EventArgs.Empty);
        }
    }
}
