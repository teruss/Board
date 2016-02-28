using System.Collections.Generic;

namespace Board
{
    public class MoveController
    {
        Stack<Command> commands = new Stack<Command>();
        Stack<Command> undidCommands = new Stack<Command>();

        public PieceModel PrevPiece
        {
            get
            {
                if (commands.Count == 0)
                    return null;
                var c = commands.Peek();
                return c.Piece;
            }
        }

        public void Move(World world, PieceModel piece, Location location)
        {
            undidCommands.Clear();
            Execute(CreateMoveCommand(world, piece, location));
        }

        public void MoveAndPromote(World controller, PieceModel piece, Location location)
        {
            undidCommands.Clear();
            var multi = new MultiCommand();
            multi.Add(CreateMoveCommand(controller, piece, location));
            multi.Add(new Promote(piece));
            Execute(multi);
        }

        Command CreateMoveCommand(World controller, PieceModel piece, Location location)
        {
            var move = new MoveCommand(piece, location);

            if (piece.captured)
            {
                var multi = new MultiCommand();
                multi.Add(new Drop(piece, controller));
                multi.Add(move);
                return multi;
            }

            foreach (var p in controller.Pieces())
            {
                if (p != piece && p.opposed != piece.opposed && !p.captured && p.Location == location)
                {
                    var multi = new MultiCommand();
                    multi.Add(new Capture(p, controller));
                    multi.Add(move);
                    return multi;
                }
            }

            return move;
        }

        void Capture(World world, PieceModel piece)
        {
            Execute(new Capture(piece, world));
        }

        void Drop(World world, PieceModel piece)
        {
            Execute(new Drop(piece, world));
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
