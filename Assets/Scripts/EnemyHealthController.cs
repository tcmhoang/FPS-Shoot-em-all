using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyHealthController : MonoBehaviour
    {
        public int CurrentHealth = 5;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void DamageEnemy(int damageAmount)
        {
            CurrentHealth-=damageAmount;
            if (CurrentHealth <= 0)
                Destroy(gameObject);
        }
    }
}