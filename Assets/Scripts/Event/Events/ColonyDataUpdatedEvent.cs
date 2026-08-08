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

        public float RemainingDaysUntilFoodRunsOut { get; private set; }

        public float RemainingDaysUntilWaterRunsOut { get; private set; }

        public int Day { get; private set; }

        public ColonyDataChangedEvent(int colonyId, int villagers, float foodReserve, float waterReserve, int day, float dailyFoodConsumption, float remainingDaysForFood, float dailyWaterConsumption, float remainingDaysForWater)
        {
            ColonyId = colonyId;
            VillagersCount = villagers;
            FoodReserve = foodReserve;
            WaterReserve = waterReserve;
            Day = day;
            FoodConsumptionPerVillagerPerDay = dailyFoodConsumption;
            WaterConsumptionPerVillagerPerDay = dailyWaterConsumption;
            RemainingDaysUntilFoodRunsOut = remainingDaysForFood;
            RemainingDaysUntilWaterRunsOut = remainingDaysForWater;
        }
    }
}
