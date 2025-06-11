using Command.Main;
namespace Command.Commands
{
    public class BerserkActionCommand : UnitCommand
    {
        private bool willHitTarget;
        public BerserkActionCommand(CommandData commandData)
        {
            this.commandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(Actions.CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override bool WillHitTarget() => true;
    }
}
