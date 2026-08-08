using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ColonySurvivalPrototype.Event;
using ColonySurvivalPrototype.Main;

namespace ColonySurvivalPrototype.UI
{
    public class ColonyUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _populationText;
        [SerializeField] private TMP_Text _foodText;
        [SerializeField] private TMP_Text _waterText;
        [SerializeField] private Button _simulationClockButtonPrefab;
        [SerializeField] private Transform _simulationButtonContainer;

        public static ColonyUIManager Instance { get; private set; }

        private EventBusService _eventBusServiceObj;
        private const float defaultNumValue = 0f;

        private void SubscribeToEvents()
        {
            _eventBusServiceObj.Subscribe<NewColonyAddedEvent>(OnNewColonyAddedEvent_UI);
    
        }

        private void UnsubscribeToEvents()
        {
            _eventBusServiceObj.Unsubscribe<NewColonyAddedEvent>(OnNewColonyAddedEvent_UI);
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
            SubscribeToEvents();
            SetDefaultValuesOnUI();
        }

        private void OnNewColonyAddedEvent_UI(NewColonyAddedEvent eventObj)
        {
            CreateSimulationButton(eventObj.ColonyId);
        }

        private void CreateSimulationButton(int colonyId)
        {
            var button = Instantiate(_simulationClockButtonPrefab, _simulationButtonContainer);

            button.onClick.AddListener(() => HandleButtonClicked(colonyId));
        }

        private void HandleButtonClicked(int colonyId)
        {
            Debug.Log($"ButtonClicked {colonyId}");
        }

        private void SetDefaultValuesOnUI()
        {
            UpdateFoodText(defaultNumValue);
            UpdateWaterText(defaultNumValue);
            UpdatePopulationText((int)defaultNumValue);
        }

        private void UpdateFoodText(float value)
        {
            _foodText.text = FormatNumber(value);
        }

        private void UpdateWaterText(float value)
        {
            _waterText.text = FormatNumber(value);
        }

        private void UpdatePopulationText(int value)
        {
            _populationText.text = FormatNumber(value);
        }

        private string FormatNumber(float value)
        {
            if (value < 10_000)
                return value.ToString();

            if (value < 1_000_000)
                return $"{value / 1000f:0.#}K";

            if (value < 1_000_000_000)
                return $"{value / 1_000_000f:0.#}M";

            return $"{value / 1_000_000_000f:0.#}B";
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
