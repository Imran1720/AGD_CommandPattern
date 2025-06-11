using Command.Main;
namespace Command.Commands
{

    public class MeditateCommand : UnitCommand
    {
        private bool willHitTarget;
        public MeditateCommand(CommandData commandData)
        {
            this.CommandData = commandData;
            willHitTarget = WillHitTarget();
        }

        public override void Execute() => GameService.Instance.ActionService.GetActionByType(Actions.CommandType.Heal).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override bool WillHitTarget() => true;
    }

}