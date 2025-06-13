using Command.Main;
using Command.UI;
namespace Command.Commands
{

    public class MeditateCommand : UnitCommand
    {
        private bool willHitTarget;
        private int previousMaxHealth;
        ActionSelectionUIController actionSelectionUIController;
        public MeditateCommand(CommandData commandData, ActionSelectionUIController actionSelectionUIController)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
            this.actionSelectionUIController = actionSelectionUIController;
        }

        public override void Execute()
        {
            previousMaxHealth = targetUnit.CurrentMaxHealth;
            GameService.Instance.ActionService.GetActionByType(Actions.CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);
        }
        public override void undo()
        {
            if (willHitTarget)
            {
                var healthToReduce = targetUnit.CurrentMaxHealth - previousMaxHealth;
                targetUnit.CurrentMaxHealth = previousMaxHealth;
                targetUnit.TakeDamage(healthToReduce);
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
            actionSelectionUIController.Show(actorUnit.GetCommandsList());
        }

        public override bool WillHitTarget() => true;
    }

}