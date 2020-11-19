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
        
        }
    }
}
