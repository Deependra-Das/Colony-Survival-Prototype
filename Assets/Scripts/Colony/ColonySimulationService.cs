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
            RaiseColonyDataChangedEvent(newColony);
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
            RaiseColonyDataChangedEvent(colony);
        }

        private void RaiseNewColonyAddedEvent(int ColonyId)
        {
            _eventBusServiceObj.Publish(new NewColonyAddedEvent(ColonyId));
        }

        private void RaiseColonyDataChangedEvent(ColonyData colony)
        {
            _eventBusServiceObj.Publish(new ColonyDataChangedEvent(colony.ColonyId, colony.VillagersCount, colony.FoodReserve, colony.WaterReserve, colony.Day));
        }
    }
}
