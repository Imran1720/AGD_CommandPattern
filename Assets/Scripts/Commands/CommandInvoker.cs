using System.Collections.Generic;

namespace Command.Commands
{

    public class CommandInvoker
    {
        private Stack<ICommand> commandRegister = new Stack<ICommand>();

        public void ProcessCommand(ICommand commandToProcess)
        {
            ExecuteCommand(commandToProcess);
            RegiterCommand(commandToProcess);
        }

        public void ExecuteCommand(ICommand commandToExecute) => commandToExecute.Execute();

        public void RegiterCommand(ICommand commandToRegiter) => commandRegister.Push(commandToRegiter);
    }

}