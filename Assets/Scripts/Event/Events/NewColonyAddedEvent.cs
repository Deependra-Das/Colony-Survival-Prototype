namespace ColonySurvivalPrototype.Event
{
    public class NewColonyAddedEvent
    {
        public int ColonyId { get; private set; }

        public NewColonyAddedEvent(int colonyId)
        {
            ColonyId = colonyId;
        }
    }
}
