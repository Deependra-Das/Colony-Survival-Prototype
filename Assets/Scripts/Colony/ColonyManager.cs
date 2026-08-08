using UnityEngine;
using System.Collections;
using ColonySurvivalPrototype.Utility;
using ColonySurvivalPrototype.Event;

namespace ColonySurvivalPrototype.Colony
{
    public class ColonyManager : MonoBehaviour
    {
        [SerializeField] private float dayDuration = 1f;

        public static ColonyManager Instance { get; private set; }

        private ColonySimulationService _colonySimulationServiceObj;
        private JsonDataLoaderService _jsonLoaderServiceObj;
        private EventBusService _eventBusServiceObj;
        private Coroutine _simulationClockCoroutine;
        PopulationData populationData;
        ConsumptionData consumptionData;

        private void SubscribeToEvents()
        {
            _eventBusServiceObj.Subscribe<SimulationClockButtonClickedEvent>(StartSimulationClock);
        }

        private void UnsubscribeToEvents()
        {
            _eventBusServiceObj.Unsubscribe<SimulationClockButtonClickedEvent>(StartSimulationClock);
        }

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

        public void Initialize(JsonDataLoaderService jsonLoaderService, ColonySimulationService colonySimulationService, EventBusService eventBusService)
        {
            _jsonLoaderServiceObj = jsonLoaderService;
            _colonySimulationServiceObj = colonySimulationService;
            _eventBusServiceObj= eventBusService;

            SubscribeToEvents();
            LoadVillageConfigData();
            AddNewColony();
        }

        private void LoadVillageConfigData()
        {
            populationData = _jsonLoaderServiceObj.Load<PopulationData>("population.json");
            consumptionData = _jsonLoaderServiceObj.Load<ConsumptionData>("consumption.json");
        }

        private void AddNewColony()
        {
            if (populationData != null && consumptionData != null)
            {
                _colonySimulationServiceObj.AddNewColony(populationData, consumptionData);
                ColonyData colonyData = _colonySimulationServiceObj.GetColonyDataByColonyId(0);
            }
            else
            {
                Debug.Log("No Config Data Loaded");
            }
        }

        private void StartSimulationClock(SimulationClockButtonClickedEvent eventObj)
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

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}