using Command.Actions;
using Command.Main;
using Command.UI;
namespace Command.Commands
{
    public class CleanseCommand : UnitCommand
    {
        private bool willHitTarget;
        private int previousPower;
        ActionSelectionUIController actionSelectionUIController;
        public CleanseCommand(CommandData commandData, ActionSelectionUIController actionSelectionUIController)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
            this.actionSelectionUIController = actionSelectionUIController;
        }
        public override void Execute()
        {
            previousPower = targetUnit.CurrentPower;
            GameService.Instance.ActionService.GetActionByType(CommandType.Cleanse).PerformAction(actorUnit, targetUnit, willHitTarget);
        }

        public override void undo()
        {
            if (willHitTarget)
                targetUnit.CurrentPower = previousPower;

            actorUnit.Owner.ResetCurrentActiveUnit();
            actionSelectionUIController.Show(actorUnit.GetCommandsList());
        }

        public override bool WillHitTarget() => true;
    }

}