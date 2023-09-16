using GamePlay.EventSystem;
using System.Collections.Generic;
using Utilities;

public class CommandManager : Singleton<CommandManager>
{
    private Queue<ICommand> _commands = new();

    public void StartExecution()
    {
        if (_commands.Count <= 0)
        {
            EventManager.Instance.TriggerEvent<AllCommandsCompleted>();
            return;
        }
        var firstCommand = _commands.Dequeue();

        firstCommand.Execute(StartExecution);
    }

    public void AddCommand(ICommand command)
    {
        _commands.Enqueue(command);
    }
}