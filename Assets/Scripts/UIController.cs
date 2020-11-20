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

        public Image DamageEffect;
        private const float _damageAlpha = 0.25f;
        private const float _damageFadeSpeed = 0.5f;

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
        }

        public void Damage()
        {
            DamageEffect.color = new Color(DamageEffect.color.r, DamageEffect.color.g, DamageEffect.color.b,
                _damageAlpha);
        }
    }
}