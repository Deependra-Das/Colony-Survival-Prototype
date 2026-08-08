namespace ColonySurvivalPrototype.Event
{
    public class SimulationClockButtonClickedEvent
    {
        public int ColonyId { get; private set; }

        public SimulationClockButtonClickedEvent(int colonyId)
        {
            ColonyId = colonyId;
        }
    }
}
