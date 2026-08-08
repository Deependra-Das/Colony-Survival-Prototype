using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ColonySurvivalPrototype.Main;
using ColonySurvivalPrototype.Event;

namespace ColonySurvivalPrototype.UI
{
    public class ColonyUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _populationText;
        [SerializeField] private TMP_Text _foodText;
        [SerializeField] private TMP_Text _waterText;
        [SerializeField] private Button _simulationClockButton;

        public static ColonyUIManager Instance { get; private set; }

        private EventBusService _eventBusServiceObj;

        private void OnEnable() => SubscribeToEvents();
        private void OnDisable() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            _simulationClockButton.onClick.AddListener(OnSimulationClockButtonClicked);
        }

        private void UnsubscribeToEvents()
        {
            _simulationClockButton.onClick.RemoveListener(OnSimulationClockButtonClicked);
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

        public void Initialize()
        {
            _eventBusServiceObj = GameManager.Instance.Services.Get<EventBusService>();

            _populationText.text = "0000";
            _foodText.text = "0000";
            _waterText.text = "0000";
        }

        private void OnSimulationClockButtonClicked()
        {
            _eventBusServiceObj.Publish(new SimulationClockButtonClickedEvent());
        }
    }
}
