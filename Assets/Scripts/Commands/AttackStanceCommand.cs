using Command.Actions;
using Command.Main;

namespace Command.Commands
{

    public class AttackStanceCommand : UnitCommand
    {
        private bool willHitTarget;

        public AttackStanceCommand(CommandData data)
        {
            this.commandData = data;
            willHitTarget = WillHitTarget();
        }
        public override void Execute() => GameService.Instance.ActionService.GetActionByType(CommandType.AttackStance).PerformAction(actorUnit, targetUnit, willHitTarget);

        public override void undo()
        {
            if (willHitTarget)
            {
                targetUnit.CurrentPower -= (int)(targetUnit.CurrentPower * .2f);
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
        }

        public override bool WillHitTarget() => true;
    }

}