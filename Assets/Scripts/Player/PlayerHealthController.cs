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
            if (_invisibleCounter > 0 || GameManager.Instance.LevelEnding) return;
            _currentHealth -= amount;
            AudioManager.Instance.PlaySfx(SoundIndex.Hurt);

            if (_currentHealth <= 0)
            {
                AudioManager.Instance.StopBgm();

                _currentHealth = 0;
                GameManager.Instance.ReloadLevel();
                gameObject.SetActive(false);
                AudioManager.Instance.StopSfx(SoundIndex.Hurt);
                AudioManager.Instance.PlaySfx(SoundIndex.Dead);
            }

            _invisibleCounter = InvisibleSpan;
            UpdateUi();
            UIController.Instance.Damage();
        }

        public void Heal(int amount)
        {
            _currentHealth = _currentHealth + amount > MaxHealth ? MaxHealth : _currentHealth + amount;
            UpdateUi();
        }

    }
}
