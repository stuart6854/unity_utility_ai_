using UnityEngine;

namespace SM
{
    public class Stats : MonoBehaviour
    {
        public int Energy
        {
            get => _energy;
            set
            {
                _energy = Mathf.Clamp(value, 0, 100);
                OnStatValueChanged?.Invoke();
            }
        }

        public int Hunger
        {
            get => _hunger;
            set
            {
                _hunger = Mathf.Clamp(value, 0, 100);
                OnStatValueChanged?.Invoke();
            }
        }

        public int Money
        {
            get => _money;
            set
            {
                _money = Mathf.Clamp(value, 0, 100);
                OnStatValueChanged?.Invoke();
            }
        }

        private int _hunger;
        private int _energy;
        private int _money;

        [SerializeField] private float timeToDecreaseHunger = 5.0f;
        [SerializeField] private float timeToDecreaseEnergy = 5.0f;
        private float _timeLeftHunger;
        private float _timeLeftEnergy;

        [SerializeField] private Billboard billboard;
        
        public delegate void StatValueChangedHandler();

        public event StatValueChangedHandler OnStatValueChanged;

        private void Start()
        {
            Hunger = Random.Range(20, 80);
            Energy = Random.Range(20, 80);
            Money = Random.Range(20, 80);
            
            // Test Case: NPC will likely work
            // Hunger = 0;
            // Energy = 100;
            // Money = 50;
            
            // Test Case: NPC will likely eat
            // Hunger = 90;
            // Energy = 50;
            // Money = 500;
            
            // Test Case: NPC will likely sleep
            // Hunger = 0;
            // Energy = 10;
            // Money = 500;
        }

        private void OnEnable()
        {
            OnStatValueChanged += UpdateDisplayText;
        }

        private void OnDisable()
        {
            OnStatValueChanged -= UpdateDisplayText;
        }

        public void UpdateHunger()
        {
            if (_timeLeftHunger > 0)
            {
                _timeLeftHunger -= Time.deltaTime;
                return;
            }

            _timeLeftHunger = timeToDecreaseHunger;
            ++Hunger;
        }

        public void UpdateEnergy(bool shouldNotUpdateEnergy)
        {
            if (shouldNotUpdateEnergy)
                return;
            
            if (_timeLeftEnergy > 0)
            {
                _timeLeftEnergy -= Time.deltaTime;
                return;
            }

            _timeLeftEnergy = timeToDecreaseEnergy;
            Energy -= 1;
        }

        private void UpdateDisplayText()
        {
            billboard.UpdateStatsText(Energy, Hunger, Money);
        }
    }
}