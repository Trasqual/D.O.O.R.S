using GamePlay.EventSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace GamePlay.CommandSystem
{
    public class CommandManager : Singleton<CommandManager>
    {
        private readonly Queue<ICommand> _commands = new();
        private bool _executionInProgress;

        private void StartExecution()
        {
            if (_executionInProgress) return;
            _executionInProgress = true;
            ExecuteCommands();
        }

        private void ExecuteCommands()
        {
            if (_commands.Count <= 0)
            {
                _executionInProgress = false;
                EventManager.Instance.TriggerEvent<AllCommandsCompleted>();
                return;
            }
            SortQueue();
            var firstCommand = _commands.Dequeue();

            firstCommand.Execute(ExecuteCommands);
        }

        public void AddCommand(ICommand command)
        {
            Debug.Log("Adde Command: " + command.GetType().ToString());
            _commands.Enqueue(command);
            if (command.Priority == RoomCreationPriority.PlayerToNewRoom)
                StartExecution();
        }

        private void SortQueue()
        {
            var commandList = new List<ICommand>();
            foreach (var command in _commands)
            {
                commandList.Add(command);
            }
            commandList.OrderBy(x => x.Priority);
            foreach (var command in commandList)
            {
                _commands.Enqueue(command);
            }
        }
    }
}