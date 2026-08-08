using UnityEngine;
using System.Collections.Generic;

namespace ColonySurvivalPrototype.Colony
{
    public class ColonySimulationService
    {
        private Dictionary<int, ColonyData> _colonyDataDictionary;

        public int ColonyCount => _colonyDataDictionary.Count;

        public ColonySimulationService()
        {
            _colonyDataDictionary = new Dictionary<int, ColonyData>();  
        }

        public void AddNewColony(PopulationData populationData, ConsumptionData consumptionData)
        {
            int newColonyId = ColonyCount;
            _colonyDataDictionary.Add(newColonyId, new ColonyData(newColonyId, populationData, consumptionData));
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
    }
}
