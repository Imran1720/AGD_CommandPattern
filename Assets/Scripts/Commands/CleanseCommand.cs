using Command.Actions;
using Command.Main;
namespace Command.Commands
{
    public class CleanseCommand : UnitCommand
    {
        private bool willHitTarget;
        public CleanseCommand(CommandData commandData)
        {
            this.CommandData = commandData;
            willHitTarget = WillHitTarget();
        }
        public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.Cleanse).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override bool WillHitTarget() => true;
    }

}