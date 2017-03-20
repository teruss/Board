using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            var list = new Stack<string>();
            foreach(var command in commands)
            {
                list.Push(command.ToString());
            }
            return string.Join("cmd", list.ToArray());
        }

        public void Load(string commandList)
        {
            var list = commandList.Split(new [] { "cmd" }, StringSplitOptions.None);
            foreach (var command in list)
            {
                commands.Push(CreateCommand(command));
            }
        }

        private Command CreateCommand(string command)
        {
            UnityEngine.Debug.Log(command);
            if (command.StartsWith("multi"))
            {
                var multi = new MultiCommand();
                //multi.Load(command.Substring(5));
                multi.Add(CreateCommand(command.Substring(5)));
                return multi;
            }
            else if (command.StartsWith("move"))
            {
                return JsonUtility.FromJson<MoveCommand>(command.Substring(4));
            }
            else if (command.StartsWith("<capture>"))
            {
                return JsonUtility.FromJson<Capture>(command.Substring(9, command.IndexOf("</capture>") - 9));
            }
            else
            {
                UnityEngine.Debug.Log(command);
            }
            return null;
        }
    }
}
