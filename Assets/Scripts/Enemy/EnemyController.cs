using System;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
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

        public float FireRate, ShootDuration = 1, ReloadTime = 2;
        private float _fireCount, _shootTime, _reloadCounter;

        public Animator Animator;


        // Start is called before the first frame update
        private void Start()
        {
            _startPoint = transform.position;
            _reloadCounter = ReloadTime;
            _shootTime = ShootDuration;
        }

        // Update is called once per frame
        private void Update()
        {
            _targetPoint = PlayerController.Instance.transform.position;
            _targetPoint.y = transform.position.y;

            float distance = Vector3.Distance(transform.position, _targetPoint);

            if (!_isChasing)
            {
                #region Start Chassing Player

                if (distance < DistanceToChase)
                {
                    _isChasing = true;

                    _shootTime = ShootDuration;
                    _reloadCounter = ReloadTime;
                }

                #endregion


                #region Check if continue chasing or not

                if (_chaseCounter > 0)
                {
                    _chaseCounter -= Time.deltaTime;
                    if (_chaseCounter <= 0)
                        Agent.destination = _startPoint;
                }

                #endregion

                Animator.SetBool("IsMoving", Agent.remainingDistance > .25f);
            }
            else
            {
                // transform.LookAt(_targetPoint);
                // Body.velocity = transform.forward * MoveSpeed;

                #region Chasing

                //Freeze
                Agent.destination = distance > DistanceToStop ? _targetPoint : transform.position;

                //Chasing
                if (distance > DistanceToLose)
                {
                    _chaseCounter = KeepTrackingTime;
                    _isChasing = false;
                }

                #endregion

                #region Shooting

                if (_reloadCounter > 0)
                {
                    _reloadCounter -= Time.deltaTime;
                    if (_reloadCounter <= 0) _shootTime = ShootDuration;
                    Animator.SetBool("IsMoving", true);
                }
                else
                {
                    if (PlayerController.Instance.gameObject.activeInHierarchy)
                        Fire();
                }

                #endregion
            }
        }

        private void Fire()
        {
            _shootTime -= Time.deltaTime;
            if (_shootTime > 0)
            {
                _fireCount -= Time.deltaTime;
                if (_fireCount > 0) return;
                _fireCount = FireRate;
                Aim();
            }
            else
                _reloadCounter = ReloadTime;
        }

        private void Aim()
        {
            Agent.destination = transform.position;
            Animator.SetBool("IsMoving", false);

            FirePoint.LookAt(PlayerController.Instance.transform.position + new Vector3(0, 1.2f, 0));
            var targetDirection = PlayerController.Instance.transform.position - transform.position;
            if (Math.Abs(Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up)) < 30)
            {
                Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
                Animator.SetTrigger("FireShot");
            }
            else
                _reloadCounter = ReloadTime;
        }
    }
}