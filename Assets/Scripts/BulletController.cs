using UnityEngine;

namespace Assets.Scripts
{
    public class BulletController : MonoBehaviour
    {
        public static float Speed = 15;
        public static float LifeTime = 5;
        private float _lifeTime;

        public Rigidbody Body;

        public GameObject Impact;

        // Start is called before the first frame update
        void Start()
        {
            _lifeTime = LifeTime;
        }

        // Update is called once per frame
        void Update()
        {
            Body.velocity = transform.forward * Speed;
            _lifeTime -= Time.deltaTime;
            if (_lifeTime < 0) Fire();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
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