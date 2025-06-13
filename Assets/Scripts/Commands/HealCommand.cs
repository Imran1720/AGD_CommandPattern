using Command.Main;
using Command.UI;

namespace Command.Commands
{

    public class HealCommand : UnitCommand
    {
        private bool willHitTarget;
        ActionSelectionUIController actionSelectionUIController;
        public HealCommand(CommandData commandData, ActionSelectionUIController actionSelectionUIController)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
            this.actionSelectionUIController = actionSelectionUIController;
        }

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(Actions.CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override void undo()
        {
            if (willHitTarget)
            {
                targetUnit.TakeDamage(actorUnit.CurrentPower);
                actorUnit.Owner.ResetCurrentActiveUnit();
                actionSelectionUIController.Show(actorUnit.GetCommandsList());
            }
        }

        public override bool WillHitTarget() => true;
    }

}