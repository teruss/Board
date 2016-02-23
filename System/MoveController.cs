using System.Collections.Generic;

namespace Board
{
    public class MoveController
    {
        Stack<Command> commands = new Stack<Command>();
        Stack<Command> undidCommands = new Stack<Command>();
        IGameController controller;

        public MoveController(IGameController controller)
        {
            this.controller = controller;
        }

        public void Move(IPiece piece, int file, int rank)
        {
            undidCommands.Clear();
            Execute(CreateMoveCommand(controller, piece, file, rank));
        }

        public void MoveAndPromote(IGameController controller, IPiece piece, int file, int rank)
        {
            undidCommands.Clear();
            var multi = new MultiCommand();
            multi.Add(CreateMoveCommand(controller, piece, file, rank));
            multi.Add(new Promote(piece));
            Execute(multi);
        }

        Command CreateMoveCommand(IGameController controller, IPiece piece, int file, int rank)
        {
            var move = new MoveCommand(piece, file, rank);

            if (piece.captured)
            {
                var multi = new MultiCommand();
                multi.Add(new Drop(piece, controller));
                multi.Add(move);
                return multi;
            }

            foreach (var p in controller.Pieces())
            {
                if (p != piece && p.opposed != piece.opposed && !p.captured && p.row == rank && p.column == file)
                {
                    var multi = new MultiCommand();
                    multi.Add(new Capture(p, controller));
                    multi.Add(move);
                    return multi;
                }
            }

            return move;
        }

        void Capture(IPiece piece)
        {
            Execute(new Capture(piece, controller));
        }

        void Drop(IPiece piece)
        {
            Execute(new Drop(piece, controller));
        }

        void Execute(Command command)
        {
            command.Execute();
            commands.Push(command);
        }

        public void Undo()
        {
            if (commands.Count == 0)
                return;

            var command = commands.Pop();
            command.Undo();
            undidCommands.Push(command);
        }

        public void UndoAll()
        {
            while (commands.Count > 0)
            {
                Undo();
            }
        }

        public void Redo()
        {
            if (undidCommands.Count == 0)
                return;

            Execute(undidCommands.Pop());
        }

        public void RedoAll()
        {
            while (undidCommands.Count > 0)
                Redo();
        }
    }
}
