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

        public override void Execute(SpriteController spriteController)
        {
            foreach (var command in commands)
                command.Execute(spriteController);
        }

        public override void Undo(SpriteController spriteController)
        {
            foreach (var command in commands)
                command.Undo(spriteController);
        }
    }
}

