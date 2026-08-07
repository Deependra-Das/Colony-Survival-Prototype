using UnityEngine;
using ColonySurvivalPrototype.Event;
using ColonySurvivalPrototype.UI;

namespace ColonySurvivalPrototype.Main
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public ServiceLocator Services { get; private set; }
        private EventBusService _eventBus;

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
        }

        private void RegisterServices()
        {
            Services.Register(_eventBus);
        }
    }
}