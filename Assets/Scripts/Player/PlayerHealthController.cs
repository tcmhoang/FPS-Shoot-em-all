using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        public static PlayerHealthController Instance;

        public int MaxHealth;
        private int _currentHealth;
        public float InvisibleSpan = 1f;
        private float _invisibleCounter;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _currentHealth = MaxHealth;
            UIController.Instance.HealthSlider.maxValue = MaxHealth;
            UpdateUi();
        }

        private void UpdateUi()
        {
            UIController.Instance.HealthSlider.value = _currentHealth;
            UIController.Instance.HealthText.text = $"HEALTH: {_currentHealth}/{MaxHealth}";
        }

        // Update is called once per frame
        private void Update()
        {
            if (_invisibleCounter > 0)
                _invisibleCounter -= Time.deltaTime;
        }

        public void Damage(int amount)
        {
            if (_invisibleCounter > 0) return;
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                GameManager.Instance.ReloadLevel();
                gameObject.SetActive(false);
            }

            _invisibleCounter = InvisibleSpan;
            UpdateUi();
        }

        public void Heal(int amount)
        {
            _currentHealth = _currentHealth + amount > MaxHealth ? MaxHealth : _currentHealth + amount;
            UpdateUi();
        }

    }
}
