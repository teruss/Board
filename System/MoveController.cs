using System.Collections.Generic;

namespace Board
{
    public class MoveController
    {
        Stack<Command> commands = new Stack<Command>();
        Stack<Command> undidCommands = new Stack<Command>();

        public void Move(World world, PieceModel piece, Location location)
        {
            undidCommands.Clear();
            Execute(CreateMoveCommand(world, piece, location));
        }

        public void MoveAndPromote(World world, PieceModel piece, Location location)
        {
            undidCommands.Clear();
            var multi = new MultiCommand();
            multi.Add(CreateMoveCommand(world, piece, location));
            multi.Add(new Promote(world, piece));
            Execute(multi);
        }

        Command CreateMoveCommand(World world, PieceModel piece, Location location)
        {
            var move = new MoveCommand(world, piece, location);

            if (piece.captured)
            {
                var multi = new MultiCommand();
                multi.Add(new Drop(piece, world));
                multi.Add(move);
                return multi;
            }

            foreach (var p in world.Pieces())
            {
                if (p != piece && p.Player != piece.Player && !p.captured && p.Location == location)
                {
                    var multi = new MultiCommand();
                    multi.Add(new Capture(p, world));
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

        public override string ToString()
        {
            var list = new List<string>();
            foreach(var command in commands)
            {
                list.Add(command.ToString());
            }
            return "[" + string.Join(",", list.ToArray()) + "]";
        }

        public void Load(string commandList)
        {

        }
    }
}
