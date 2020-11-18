using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerHealthController : MonoBehaviour
    {
        public static PlayerHealthController Instance;

        public int MaxHealth;
        private int _currentHealth;

        public void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _currentHealth = MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
