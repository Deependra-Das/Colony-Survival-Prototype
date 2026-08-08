using UnityEngine;
using System.Collections;
using ColonySurvivalPrototype.Utility;

namespace ColonySurvivalPrototype.Colony
{
    public class ColonyManager : MonoBehaviour
    {
        [SerializeField] private float dayDuration = 1f;

        public static ColonyManager Instance { get; private set; }

        private ColonySimulationService _colonySimulationServiceObj;
        private JsonDataLoaderService _jsonLoaderServiceObj;
        private Coroutine _simulationClockCoroutine;

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
                Debug.Log($"ColonyId: {colonyData.ColonyId}, VillagerCount: {colonyData.VillagersCount}, Food: {colonyData.FoodReserve}, Water: {colonyData.WaterReserve}," +
                    $"Food Consumption Per Villager Per Day: {colonyData.FoodConsumptionPerVillagerPerDay}, Water Consumption Per Villager Per Day: {colonyData.WaterConsumptionPerVillagerPerDay}, Day: {colonyData.Day}  ");

                StartSimulationClock();
            }
            else
            {
                Debug.Log("No Config Data Loaded");
            }               
        }

        private void StartSimulationClock()
        {
            _simulationClockCoroutine = StartCoroutine(SimulationClock());
        }

        private IEnumerator SimulationClock()
        {
            while (true)
            {
                yield return new WaitForSeconds(dayDuration);

                _colonySimulationServiceObj.AdvanceOneDayForVillageById(0);
            }
        }

        private void StopSimulationClock()
        {
            if(_simulationClockCoroutine!=null)
            {
                StopCoroutine(_simulationClockCoroutine);
                _simulationClockCoroutine = null;
            }
        }
    }
}