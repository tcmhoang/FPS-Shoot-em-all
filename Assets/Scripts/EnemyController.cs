using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {


        private bool _isChasing;
        public float DistanceToChase = 10f, DistanceToLose = 15f, DistanceToStop = 2f;

        private Vector3 _targetPoint;
        public NavMeshAgent Agent;

        private Vector3 _startPoint;
        public float KeepTrackingTime = 5f;
        private float _chaseCounter;

        public GameObject Bullet;
        public Transform FirePoint;

        public float FireRate;
        private float _fireCount;


        // Start is called before the first frame update
        void Start()
        {
            _startPoint = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            _targetPoint = PlayerController.Instance.transform.position;
            _targetPoint.y = transform.position.y;

            float distance = Vector3.Distance(transform.position, _targetPoint);
            if (!_isChasing)
            {
                if (distance < DistanceToChase)
                {
                    _isChasing = true;
                    _fireCount = 1;
                }

                if (_chaseCounter < 0) return;
                _chaseCounter -= Time.deltaTime;
                if (_chaseCounter <= 0)
                {
                    Agent.destination = _startPoint;
                }
            }
            else
            {
                // transform.LookAt(_targetPoint);
                // Body.velocity = transform.forward * MoveSpeed;

                //Stop
                Agent.destination = distance > DistanceToStop ? _targetPoint : transform.position;

                //Chasing
                if (distance > DistanceToLose)
                {
                    _chaseCounter = KeepTrackingTime;
                    _isChasing = false;
                }

                _fireCount -= Time.deltaTime;
                if (!(_fireCount <= 0)) return;
                _fireCount = FireRate;
                Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
            }

           
        }
    }
}