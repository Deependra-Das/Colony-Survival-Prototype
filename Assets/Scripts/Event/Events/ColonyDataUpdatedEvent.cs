namespace ColonySurvivalPrototype.Event
{
    public class ColonyDataChangedEvent
    {
        public int ColonyId { get; private set; }

        public int VillagersCount { get; private set; }

        public float FoodReserve { get; private set; }

        public float WaterReserve { get; private set; }

        public float FoodConsumptionPerVillagerPerDay { get; private set; }

        public float WaterConsumptionPerVillagerPerDay { get; private set; }

        public int Day { get; private set; }

        public ColonyDataChangedEvent(int colonyId, int villagers, float foodReserve, float waterReserve, int day)
        {
            ColonyId = colonyId;
            VillagersCount = villagers;
            FoodReserve = foodReserve;
            WaterReserve = waterReserve;
            Day = day;
        }
    }
}
