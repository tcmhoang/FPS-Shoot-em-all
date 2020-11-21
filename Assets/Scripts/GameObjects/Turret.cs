using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class Turret : MonoBehaviour
    {
        public GameObject Bullet;

        public float RangeToTargetPlayer, TimeBetweenShot = .5f;
        private float _shotCounter;

        public Transform Gun, FirePoint;

        private float _rotationSpeed = 3;

        // Start is called before the first frame update
        private void Start()
        {
            _shotCounter = TimeBetweenShot;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <
                RangeToTargetPlayer)
            {
                Gun.LookAt(PlayerController.Instance.transform.position + new Vector3(0,1.2f,0));
                _shotCounter -= Time.deltaTime;
                if (_shotCounter > 0) return;
                Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
                _shotCounter = TimeBetweenShot;
            }
            else
            {
                _shotCounter = TimeBetweenShot;
                Gun.rotation = Quaternion.Lerp(Gun.rotation, Quaternion.Euler(0, Gun.rotation.eulerAngles.y + 10, 0),
                    _rotationSpeed * Time.deltaTime);
            }
        }
    }
}