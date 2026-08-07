using UnityEngine;
using ColonySurvivalPrototype.Event;
using ColonySurvivalPrototype.UI;
using ColonySurvivalPrototype.Utility;
using ColonySurvivalPrototype.Colony;

namespace ColonySurvivalPrototype.Main
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public ServiceLocator Services { get; private set; }
        private EventBusService _eventBus;
        private JsonDataLoaderService _jsonLoaderService;
        private ColonySimulationService _colonySimulationService;

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

        private void Start()
        {
            InitializeServices();
            RegisterServices();
            ColonyUIManager.Instance.Initialize();
        }

        private void InitializeServices()
        {
            Services = new ServiceLocator();
            _eventBus = new EventBusService();
            _jsonLoaderService = new JsonDataLoaderService();
            _colonySimulationService = new ColonySimulationService();
        }

        private void RegisterServices()
        {
            Services.Register(_eventBus);
            Services.Register(_jsonLoaderService);
            Services.Register(_colonySimulationService);
        }
    }
}