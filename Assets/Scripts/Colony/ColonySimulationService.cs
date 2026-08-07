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

        public void AddNewColony(PopulationData populationData, ConsumptionData consumption)
        {
            int newColonyId = ColonyCount;
            _colonyDataDictionary.Add(newColonyId, new ColonyData(newColonyId, populationData.villagersCount, populationData.foodReserve, populationData.waterReserve));
        }

        public ColonyData GetColonyDataByColonyId(int ColonyId)
        {
            _colonyDataDictionary.TryGetValue(ColonyId, out var colonyData);

            return colonyData;
        }
    }
}
