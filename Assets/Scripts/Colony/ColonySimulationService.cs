using UnityEngine;
using System.Collections.Generic;
using ColonySurvivalPrototype.Event;

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
            RaiseNewColonyAddedEvent(newColony.ColonyId);
            Debug.Log($"ColonyId: {newColony.ColonyId}, VillagerCount: {newColony.VillagersCount}, Food: {newColony.FoodReserve}, Water: {newColony.WaterReserve}, Day: {newColony.Day}");
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

            Debug.Log($"ColonyId: {colony.ColonyId}, VillagerCount: {colony.VillagersCount}, Food: {colony.FoodReserve}, Water: {colony.WaterReserve}, Day: {colony.Day}");
        }
        private void RaiseNewColonyAddedEvent(int ColonyId)
        {
            _eventBusServiceObj.Publish(new NewColonyAddedEvent(ColonyId));
        }
    }
}
