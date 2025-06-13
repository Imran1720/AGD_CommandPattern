using Command.Player;

namespace Command.Input
{
    internal class CreateCommandData
    {
        private UnitController targetUnit;

        public CreateCommandData(UnitController targetUnit)
        {
            this.targetUnit = targetUnit;
        }
    }
}