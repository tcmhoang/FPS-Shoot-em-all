using UnityEngine;

namespace Assets.Scripts
{
    public class BulletController : MonoBehaviour
    {
        public int Damage = 1;

        public static float Speed = 15;
        public static float LifeTime = 5;
        private float _lifeTime;

        public Rigidbody Body;

        public GameObject Impact;

        public bool DamageEnemy,DamagePlayer;

        // Start is called before the first frame update
        private void Start()
        {
            _lifeTime = LifeTime;
        }

        // Update is called once per frame
        private void Update()
        {
            Body.velocity = transform.forward * Speed;
            _lifeTime -= Time.deltaTime;
            if (_lifeTime < 0) Fire();
        }

        private void OnTriggerEnter(Component other)
        {
            if (other.gameObject.tag == "Enemy" && DamageEnemy)
            {
               other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(Damage);
            }

            if (other.gameObject.tag == "HeadShot" && DamageEnemy)
            {
                other.transform.parent.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(Damage * 2);
            }

            if (other.gameObject.tag == "Player" && DamagePlayer)
            {
                PlayerHealthController.Instance.DamagePlayer(Damage);
            }

            Fire();
        }

        private void Fire()
        {
            Destroy(gameObject);
            Instantiate(Impact, transform.position - transform.forward * Time.deltaTime * Speed, transform.rotation);
        }
    }
}