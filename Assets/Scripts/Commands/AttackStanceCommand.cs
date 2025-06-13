using Command.Actions;
using Command.Main;
using Command.UI;

namespace Command.Commands
{

    public class AttackStanceCommand : UnitCommand
    {
        private bool willHitTarget;
        ActionSelectionUIController actionSelectionUIController;

        public AttackStanceCommand(CommandData data, ActionSelectionUIController actionSelectionUIController)
        {
            this.commandData = data;
            willHitTarget = WillHitTarget();
            this.actionSelectionUIController = actionSelectionUIController;
        }
        public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.AttackStance).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override void undo()
        {
            if (willHitTarget)
            {
                targetUnit.CurrentPower -= (int)(targetUnit.CurrentPower * .2f);
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
            actionSelectionUIController.Show(actorUnit.GetCommandsList());
        }

        public override bool WillHitTarget() => true;
    }

}