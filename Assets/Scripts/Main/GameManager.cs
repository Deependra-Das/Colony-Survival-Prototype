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
        private EventBusService _eventBusService;
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
            ColonyManager.Instance.Initialize(_jsonLoaderService, _colonySimulationService, _eventBusService);
        }

        private void InitializeServices()
        {
            Services = new ServiceLocator();
            _eventBusService = new EventBusService();
            _jsonLoaderService = new JsonDataLoaderService();
            _colonySimulationService = new ColonySimulationService(_eventBusService);
        }

        private void RegisterServices()
        {
            Services.Register(_eventBusService);
            Services.Register(_jsonLoaderService);
            Services.Register(_colonySimulationService);
        }
    }
}