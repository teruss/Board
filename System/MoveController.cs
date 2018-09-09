using System;
using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class MoveController
    {
        Stack<Command> undoCommands = new Stack<Command>();
        Stack<Command> redoCommands = new Stack<Command>();

        public Stack<Command> UndoCommands { get { return undoCommands; } }
        public Stack<Command> RedoCommands { get { return redoCommands; } }

        public void Move(World world, PieceModel piece, Location location)
        {
            redoCommands.Clear();
            Execute(CreateMoveCommand(world, piece, location));
        }

        public void MoveAndPromote(World world, PieceModel piece, Location location)
        {
            redoCommands.Clear();
            var multi = new MultiCommand();
            multi.Add(new Promote(world, piece));
            multi.Add(CreateMoveCommand(world, piece, location));
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

        void Execute(Command command)
        {
            command.Execute();
            undoCommands.Push(command);
        }

        public void Undo()
        {
            var command = undoCommands.Pop();
            command.Undo();
            redoCommands.Push(command);
        }

        public void UndoAll()
        {
            while (undoCommands.Count > 0)
                Undo();
        }

        public void Redo()
        {
            Execute(redoCommands.Pop());
        }

        public void RedoAll()
        {
            while (redoCommands.Count > 0)
                Redo();
        }

        public override string ToString()
        {
            var list = new Stack<string>();
            foreach (var command in undoCommands)
            {
                list.Push(command.ToString());
            }
            return string.Join("cmd", list.ToArray());
        }

        public void Load(World world, string commandList)
        {
            var list = commandList.Split(new[] { "cmd" }, StringSplitOptions.None);
            foreach (var command in list)
            {
                Execute(CreateCommand(world, command));
            }
            UndoAll();
        }

        private Command CreateCommand(World world, string command)
        {
            if (command.StartsWith("<multi>"))
            {
                var multi = new MultiCommand();

                var s = command.Substring(7, command.LastIndexOf("</multi>") - 7);
                int m = 0;
                int first = 0;
                for (int index = 0; index < s.Length; index++)
                {
                    if (s.Substring(index, 5) == "<mlt>")
                    {
                        if (m == 0)
                        {
                            first = index + 5;
                        }
                        m++;
                    }
                    if (s.Substring(index, 6) == "</mlt>")
                    {
                        m--;
                    }

                    if (m == 0)
                    {
                        multi.Add(CreateCommand(world, s.Substring(first, index - first)));
                        index += 5;
                    }
                }
                return multi;
            }
            else if (command.StartsWith("move"))
            {
                var move = JsonUtility.FromJson<MoveCommand>(command.Substring(4));
                var piece = world.GetPiece(move.Piece.Id);
                return new MoveCommand(world, piece, move.location);
            }
            else if (command.StartsWith("<capture>"))
            {
                var capture = JsonUtility.FromJson<Capture>(command.Substring(9, command.IndexOf("</capture>") - 9));
                var piece = world.GetPiece(capture.Piece.Id);
                return new Capture(piece, world);
            }
            else if (command.StartsWith("<drop>"))
            {
                var drop = JsonUtility.FromJson<Drop>(command.Substring(6, command.IndexOf("</drop>") - 6));
                return new Drop(world.GetPiece(drop.Piece.Id), world);
            }
            else if(command.StartsWith("<promote>"))
            {
                var promote = JsonUtility.FromJson<Promote>(command.Substring(9, command.IndexOf("</promote>") - 9));
                var piece = world.GetPiece(promote.Piece.Id);
                return new Promote(world, piece);
            }
            else
            {
                UnityEngine.Debug.Log(command);
            }
            return null;
        }
    }
}
