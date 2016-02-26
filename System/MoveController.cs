using System.Collections.Generic;

namespace Board
{
    public class MoveController
    {
        Stack<Command> commands = new Stack<Command>();
        Stack<Command> undidCommands = new Stack<Command>();
        World controller;

        public MoveController(World controller)
        {
            this.controller = controller;
        }

        public void Move(SpriteController spriteController, PieceModel piece, int file, int rank)
        {
            undidCommands.Clear();
            Execute(spriteController, CreateMoveCommand(controller, piece, file, rank));
        }

        public void MoveAndPromote(World controller, PieceModel piece, int file, int rank)
        {
            undidCommands.Clear();
            var multi = new MultiCommand();
            multi.Add(CreateMoveCommand(controller, piece, file, rank));
            multi.Add(new Promote(piece));
            Execute(controller.SpriteController, multi);
        }

        Command CreateMoveCommand(World controller, PieceModel piece, int file, int rank)
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

        void Capture(PieceModel piece)
        {
            Execute(controller.SpriteController, new Capture(piece, controller));
        }

        void Drop(PieceModel piece)
        {
            Execute(controller.SpriteController, new Drop(piece, controller));
        }

        void Execute(SpriteController spriteController, Command command)
        {
            command.Execute(spriteController);
            commands.Push(command);
        }

        public void Undo()
        {
            if (commands.Count == 0)
                return;

            var command = commands.Pop();
            command.Undo(controller.SpriteController);
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

            Execute(controller.SpriteController, undidCommands.Pop());
        }

        public void RedoAll()
        {
            while (undidCommands.Count > 0)
                Redo();
        }
    }
}
