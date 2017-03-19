using System.Collections.Generic;

namespace Board
{
    public class MultiCommand : Command
    {
        List<Command> commands = new List<Command>();

        public void Add(Command command)
        {
            commands.Add(command);
        }

        public override void Execute()
        {
            foreach (var command in commands)
                command.Execute();
        }

        public override void Undo()
        {
            foreach (var command in commands)
                command.Undo();
        }

        public override string ToString()
        {
            var list = new List<string>();
            foreach (var command in commands)
            {
                list.Add(command.ToString());
            }
            return "{\"type\":\"multi\",\"data\":[" + string.Join(",", list.ToArray()) + "]}";
        }
    }
}

