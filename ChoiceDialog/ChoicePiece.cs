using System;

namespace Board
{
    public class ChoicePiece
    {
        public event EventHandler Executed;

        public void Execute()
        {
            if (Executed != null)
                Executed(this, EventArgs.Empty);
        }
    }
}
