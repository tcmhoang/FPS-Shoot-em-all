using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        public Slider HealthSlider;
        public Text HealthText;

        public Text AmmoText;

        public Image BlackGroundTransition;

        public Image DamageEffect;
        private const float _damageAlpha = 0.35f;
        private const float _damageFadeSpeed = 0.3f;

        public GameObject PauseScreen;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (DamageEffect.color.a != 0)
            {
                DamageEffect.color = new Color(DamageEffect.color.r, DamageEffect.color.g, DamageEffect.color.b,
                    Mathf.MoveTowards(DamageEffect.color.a, 0, _damageFadeSpeed * Time.deltaTime));
            }

            if (GameManager.Instance.LevelEnding)
                BlackGroundTransition.color = new Color(BlackGroundTransition.color.r, BlackGroundTransition.color.g,
                    BlackGroundTransition.color.b,
                    Mathf.MoveTowards(BlackGroundTransition.color.a, 1, _damageFadeSpeed * Time.deltaTime));
            else
                BlackGroundTransition.color = new Color(BlackGroundTransition.color.r, BlackGroundTransition.color.g,
                    BlackGroundTransition.color.b,
                    Mathf.MoveTowards(BlackGroundTransition.color.a, 0, _damageFadeSpeed * Time.deltaTime));
        }

        public void Damage()
        {
            DamageEffect.color = new Color(DamageEffect.color.r, DamageEffect.color.g, DamageEffect.color.b,
                _damageAlpha);
        }
    }
}