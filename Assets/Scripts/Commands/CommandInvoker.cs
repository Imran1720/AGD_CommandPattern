using Command.Main;
using Command.UI;
using System;
using System.Collections.Generic;

namespace Command.Commands
{

    public class CommandInvoker
    {
        private Stack<ICommand> commandRegister = new Stack<ICommand>();

        public CommandInvoker() => SubscribeToEvents();

        private void SubscribeToEvents() => GameService.Instance.EventService.OnReplayButtonSelected.AddListener(SetReplayStack);



        public void ProcessCommand(ICommand commandToProcess)
        {
            ExecuteCommand(commandToProcess);
            RegiterCommand(commandToProcess);
        }

        public void ExecuteCommand(ICommand commandToExecute) => commandToExecute.Execute();

        public void RegiterCommand(ICommand commandToRegiter) => commandRegister.Push(commandToRegiter);

        public void Undo()
        {
            if (!RegistryEmpty() && CommandBelongsToActivePlayer())
            {
                ICommand command = commandRegister.Pop();
                command.undo();
            }
        }

        public void SetReplayStack()
        {
            GameService.Instance.ReplayService.SetCommandStack(commandRegister);
            commandRegister.Clear();
        }

        private bool RegistryEmpty() => commandRegister.Count == 0;

        private bool CommandBelongsToActivePlayer()
        {
            return (commandRegister.Peek() as UnitCommand).commandData.ActorPlayerID == GameService.Instance.PlayerService.ActivePlayerID;
        }
    }

}