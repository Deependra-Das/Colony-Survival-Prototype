using UnityEngine;
using ColonySurvivalPrototype.Utility;

namespace ColonySurvivalPrototype.Colony
{
    public class ColonyManager : MonoBehaviour
    {
        public static ColonyManager Instance { get; private set; }
        private ColonySimulationService _colonySimulationServiceObj;
        private JsonDataLoaderService _jsonLoaderServiceObj;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Initialize(JsonDataLoaderService jsonLoaderService, ColonySimulationService colonySimulationService)
        {
            _jsonLoaderServiceObj = jsonLoaderService;
            _colonySimulationServiceObj = colonySimulationService;

            PopulationData populationData = _jsonLoaderServiceObj.Load<PopulationData>("population.json");
            ConsumptionData consumptionData = _jsonLoaderServiceObj.Load<ConsumptionData>("consumption.json");

            if (populationData != null && consumptionData != null)
            {
                _colonySimulationServiceObj.AddNewColony(populationData, consumptionData);
                ColonyData colonyData = _colonySimulationServiceObj.GetColonyDataByColonyId(0);
                Debug.Log($"ColonyId: {colonyData.ColonyId}, VillagerCount: {colonyData.VillagersCount}, Food: {colonyData.FoodReserve}, Water: {colonyData.WaterReserve}, Day: {colonyData.Day}  ");
            }
            else
            {
                Debug.Log("No Config Data Loaded");
            }               
        }
    }
}