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
        [SerializeField] private TMP_Text _dayText;
        [SerializeField] private TMP_Text _dailyFoodConsumptionText;
        [SerializeField] private TMP_Text _dailyWaterConsumptionText;
        [SerializeField] private TMP_Text _remainingDaysForFoodText;
        [SerializeField] private TMP_Text _remainingDaysForWaterText;
        [SerializeField] private Button _simulationClockButtonPrefab;
        [SerializeField] private Transform _simulationButtonContainer;
        [SerializeField] private GameObject _starvationAlertPopup;

        public static ColonyUIManager Instance { get; private set; }

        private EventBusService _eventBusServiceObj;
        private const float _defaultNumValue = 0f;
        private bool _showColonyStarvingPopup = false;

        private void SubscribeToEvents()
        {
            _eventBusServiceObj.Subscribe<NewColonyAddedEvent>(OnNewColonyAddedEvent_UI);
            _eventBusServiceObj.Subscribe<ColonyDataChangedEvent>(OnColonyDataChangedEvent_UI);
            _eventBusServiceObj.Subscribe<ColonyStarvingAlertEvent>(OnColonyStarvingAlertEvent_UI);
        }

        private void UnsubscribeToEvents()
        {
            _eventBusServiceObj.Unsubscribe<NewColonyAddedEvent>(OnNewColonyAddedEvent_UI);
            _eventBusServiceObj.Unsubscribe<ColonyDataChangedEvent>(OnColonyDataChangedEvent_UI);
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
            ToggleStarvationAlertPopup(false);
        }

        private void OnNewColonyAddedEvent_UI(NewColonyAddedEvent eventObj)
        {
            CreateSimulationButton(eventObj.ColonyId);
        }

        private void CreateSimulationButton(int colonyId)
        {
            var button = Instantiate(_simulationClockButtonPrefab, _simulationButtonContainer);

            button.onClick.AddListener(() => HandleButtonClicked(button, colonyId));
        }

        private void HandleButtonClicked(Button button, int colonyId)
        {
            button.interactable = false;
            OnSimulationClockButtonClicked(colonyId);
        }

        private void OnSimulationClockButtonClicked(int colonyId)
        {
            _eventBusServiceObj.Publish(new SimulationClockButtonClickedEvent(colonyId));
        }

        private void OnColonyDataChangedEvent_UI(ColonyDataChangedEvent eventObj)
        {
            UpdateFoodText(eventObj.FoodReserve);
            UpdateWaterText(eventObj.WaterReserve);
            UpdatePopulationText(eventObj.VillagersCount);


            UpdateDayText(eventObj.Day);
            UpdateDailyFoodConsumptionText(eventObj.FoodConsumptionPerVillagerPerDay);
            UpdateDailyWaterConsumptionText(eventObj.WaterConsumptionPerVillagerPerDay);
            UpdateRemainingDaysForFoodText(eventObj.RemainingDaysUntilFoodRunsOut);
            UpdateRemainingDaysForWaterText(eventObj.RemainingDaysUntilWaterRunsOut);
        }

        private void SetDefaultValuesOnUI()
        {
            UpdateFoodText(_defaultNumValue);
            UpdateWaterText(_defaultNumValue);
            UpdatePopulationText((int)_defaultNumValue);

            UpdateDayText((int)_defaultNumValue);
            UpdateDailyFoodConsumptionText(_defaultNumValue);
            UpdateDailyWaterConsumptionText(_defaultNumValue);
            UpdateRemainingDaysForFoodText(_defaultNumValue);
            UpdateRemainingDaysForWaterText(_defaultNumValue);
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

        private void UpdateDayText(int value)
        {
            _dayText.text = FormatNumber(value);
        }

        private void UpdateDailyFoodConsumptionText(float value)
        {
            _dailyFoodConsumptionText.text = value.ToString("F2");
        }

        private void UpdateDailyWaterConsumptionText(float value)
        {
            _dailyWaterConsumptionText.text = value.ToString("F2");
        }

        private void UpdateRemainingDaysForFoodText(float value)
        {
            _remainingDaysForFoodText.text = Mathf.RoundToInt(value).ToString();
        }

        private void UpdateRemainingDaysForWaterText(float value)
        {
            _remainingDaysForWaterText.text = Mathf.RoundToInt(value).ToString();
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

        private void OnColonyStarvingAlertEvent_UI(ColonyStarvingAlertEvent eventObj)
        {
            if(_showColonyStarvingPopup)
            {
                return;
            }
            _showColonyStarvingPopup = true;
            ToggleStarvationAlertPopup(true);
        }

        private void ToggleStarvationAlertPopup(bool value)
        {
            _starvationAlertPopup.SetActive(value);
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
