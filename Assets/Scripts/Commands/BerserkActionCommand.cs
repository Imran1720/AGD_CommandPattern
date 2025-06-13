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
        }

        public override bool WillHitTarget() => true;
    }
}
