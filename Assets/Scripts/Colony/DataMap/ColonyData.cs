namespace ColonySurvivalPrototype.Colony
{
    public class ColonyData
    {
        public int ColonyId { get; private set; }

        public int VillagersCount { get; private set; }

        public float FoodReserve { get; private set; }

        public float WaterReserve { get; private set; }

        public int Day { get; private set; }

        public bool IsColonyStarving => FoodReserve <= 0 || WaterReserve <= 0;

        public ColonyData(int colonyId, int villagersCount, float food, float water)
        {
            ColonyId = colonyId;
            VillagersCount = villagersCount;
            FoodReserve = food;
            WaterReserve = water;
            Day = 0;
        }
    }
}
