using Command.Main;
using Command.UI;
namespace Command.Commands
{
    public class BerserkActionCommand : UnitCommand
    {
        private bool willHitTarget;
        ActionSelectionUIController actionSelectionUIController;
        public BerserkActionCommand(CommandData commandData, ActionSelectionUIController actionSelectionUIController)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
            this.actionSelectionUIController = actionSelectionUIController;
        }

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(Actions.CommandType.BerserkAttack).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override void undo()
        {
            if (willHitTarget)
            {
                if (!targetUnit.IsAlive())
                {
                    targetUnit.Revive();
                }
                targetUnit.RestoreHealth(actorUnit.CurrentPower * 2);
            }
            else
            {
                if (!actorUnit.IsAlive())
                {
                    actorUnit.Revive();
                }
                actorUnit.RestoreHealth(actorUnit.CurrentPower * 2);
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
            actionSelectionUIController.Show(actorUnit.GetCommandsList());
        }

        public override bool WillHitTarget() => true;
    }
}
