using Command.Actions;
using Command.Main;
using Command.UI;

namespace Command.Commands
{
    public class AttackCommand : UnitCommand
    {
        private bool willHitTarget;
        ActionSelectionUIController actionSelectionUIController;

        public AttackCommand(CommandData commandData, ActionSelectionUIController actionSelectionUIController)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
            this.actionSelectionUIController = actionSelectionUIController;
        }

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Attack).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override bool WillHitTarget() => true;

        public override void undo()
        {
            if (willHitTarget)
            {
                if (!targetUnit.IsAlive())
                {
                    targetUnit.Revive();
                }
                targetUnit.RestoreHealth(actorUnit.CurrentPower);
                actorUnit.Owner.ResetCurrentActiveUnit();

                actionSelectionUIController.Show(actorUnit.GetCommandsList());

            }
        }

    }
}
