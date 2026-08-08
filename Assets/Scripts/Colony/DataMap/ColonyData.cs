namespace ColonySurvivalPrototype.Colony
{
    public class ColonyData
    {
        public int ColonyId { get; private set; }

        public int VillagersCount { get; private set; }

        public float FoodReserve { get; private set; }

        public float WaterReserve { get; private set; }

        public float FoodConsumptionPerVillagerPerDay { get; private set; }

        public float WaterConsumptionPerVillagerPerDay { get; private set; }

        public int Day { get; private set; }

        public bool IsColonyStarving => FoodReserve <= 0 || WaterReserve <= 0;

        public ColonyData(int colonyId, PopulationData populationData, ConsumptionData consumptionData)
        {
            ColonyId = colonyId;
            VillagersCount = populationData.villagersCount;
            FoodReserve = populationData.foodReserve;
            WaterReserve = populationData.waterReserve;
            FoodConsumptionPerVillagerPerDay = consumptionData.foodConsumptionPerVillagerPerDay;
            WaterConsumptionPerVillagerPerDay = consumptionData.waterConsumptionPerVillagerPerDay;
            Day = 0;
        }

        public void UpdateVillagersCount(int villagersToAdd)
        {
            VillagersCount += villagersToAdd;
        }

        public void UpdateFoodReserve(float FoodToAdd)
        {
            FoodReserve += FoodToAdd;
        }

        public void UpdateWaterReserve(float WaterToAdd)
        {
            WaterReserve += WaterToAdd;
        }

        public void UpdateFoodConsumptionPerVillagerPerDay(float value)
        {
            FoodConsumptionPerVillagerPerDay += value;
        }

        public void UpdateWaterConsumptionPerVillagerPerDay(float value)
        {
            WaterConsumptionPerVillagerPerDay += value;
        }
    }
}
