using UnityEngine;
using TMPro;

namespace ColonySurvivalPrototype.UI
{
    public class ColonyUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _populationText;
        [SerializeField] private TMP_Text _foodText;
        [SerializeField] private TMP_Text _waterText;

        public static ColonyUIManager Instance { get; private set; }

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
            _populationText.text = "0000";
            _foodText.text = "0000";
            _waterText.text = "0000";
        }
    }
}
