using Command.Actions;
using Command.Main;
namespace Command.Commands
{
    public class CleanseCommand : UnitCommand
    {
        private bool willHitTarget;
        private int previousPower;
        public CleanseCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
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
        }

        public override bool WillHitTarget() => true;
    }

}