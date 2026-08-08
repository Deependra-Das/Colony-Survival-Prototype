using ColonySurvivalPrototype.Event;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace ColonySurvivalPrototype.Colony
{
    public class ColonySimulationService
    {
        private Dictionary<int, ColonyData> _colonyDataDictionary;
        private EventBusService _eventBusServiceObj;

        public int ColonyCount => _colonyDataDictionary.Count;

        public ColonySimulationService(EventBusService eventBusService)
        {
            _eventBusServiceObj = eventBusService;
            _colonyDataDictionary = new Dictionary<int, ColonyData>();
        }

        public void AddNewColony(PopulationData populationData, ConsumptionData consumptionData)
        {
            int newColonyId = ColonyCount;
            ColonyData newColony = new ColonyData(newColonyId, populationData, consumptionData);
            _colonyDataDictionary.Add(newColonyId, newColony);

            float remainingDaysForFood = GetRemainingDaysUntilFoodRunsOut(newColony);
            float remainingDaysForWater = GetRemainingDaysUntilWaterRunsOut(newColony);

            RaiseNewColonyAddedEvent(newColony.ColonyId);
            RaiseColonyDataChangedEvent(newColony, remainingDaysForFood, remainingDaysForWater);
        }

        public ColonyData GetColonyDataByColonyId(int ColonyId)
        {
            _colonyDataDictionary.TryGetValue(ColonyId, out var colonyData);

            return colonyData;
        }

        public void AdvanceOneDayForVillageById(int villageId)
        {
            ColonyData colony = GetColonyDataByColonyId(villageId);

            float foodUsed = colony.VillagersCount * colony.FoodConsumptionPerVillagerPerDay;
            float waterUsed = colony.VillagersCount * colony.WaterConsumptionPerVillagerPerDay;

            colony.AdvanceToNextDay(foodUsed, waterUsed);
            float remainingDaysForFood = GetRemainingDaysUntilFoodRunsOut(colony);
            float remainingDaysForWater = GetRemainingDaysUntilWaterRunsOut(colony);

            RaiseColonyDataChangedEvent(colony, remainingDaysForFood, remainingDaysForWater);
        }

        public float GetRemainingDaysUntilFoodRunsOut(ColonyData colony)
        {
            float totlaDailyConsumption = colony.VillagersCount * colony.FoodConsumptionPerVillagerPerDay;

            if (totlaDailyConsumption <= 0)
                return float.PositiveInfinity;

            return colony.FoodReserve / totlaDailyConsumption;
        }

        public float GetRemainingDaysUntilWaterRunsOut(ColonyData colony)
        {
            float totlaDailyConsumption = colony.VillagersCount * colony.WaterConsumptionPerVillagerPerDay;

            if (totlaDailyConsumption <= 0)
                return float.PositiveInfinity;

            return colony.WaterReserve / totlaDailyConsumption;
        }

        private void RaiseNewColonyAddedEvent(int ColonyId)
        {
            _eventBusServiceObj.Publish(new NewColonyAddedEvent(ColonyId));
        }

        private void RaiseColonyDataChangedEvent(ColonyData colony, float remainingDaysForFood, float remainingDaysForWater)
        {
            _eventBusServiceObj.Publish(new ColonyDataChangedEvent(colony.ColonyId, colony.VillagersCount, colony.FoodReserve, colony.WaterReserve, colony.Day,
                colony.FoodConsumptionPerVillagerPerDay, remainingDaysForFood, colony.WaterConsumptionPerVillagerPerDay, remainingDaysForWater));
        }
    }
}
