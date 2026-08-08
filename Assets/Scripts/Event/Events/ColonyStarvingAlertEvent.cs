namespace ColonySurvivalPrototype.Event
{
    public class ColonyStarvingAlertEvent
    {
        public int ColonyId { get; private set; }

        public ColonyStarvingAlertEvent(int colonyId)
        {
            ColonyId = colonyId;
        }
    }
}
