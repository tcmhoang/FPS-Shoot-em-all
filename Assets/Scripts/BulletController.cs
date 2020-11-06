using UnityEngine;

namespace Assets.Scripts
{
    public class BulletController : MonoBehaviour
    {
        public float Speed = 15;

        public Rigidbody Body;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Body.velocity = transform.forward * Speed;
        }

    }
}
