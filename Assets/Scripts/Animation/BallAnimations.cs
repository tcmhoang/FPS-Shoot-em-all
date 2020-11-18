using UnityEngine;

namespace Assets.Scripts.Animation
{
    public class BallAnimations : MonoBehaviour
    {
        public bool IsMove, IsRotate;

        public float Speed;

        private bool _resetMove;

        public float MoveTime;

        private float _initMoveTime;

        // Start is called before the first frame update
        void Start()
        {
            _initMoveTime = MoveTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsMove)
            {
                MoveTime -= Time.deltaTime;
                if (MoveTime < 0)
                {
                    MoveTime = _initMoveTime;
                    _resetMove = !_resetMove;
                }
                transform.position += _resetMove ?  new Vector3(Speed,0f,0f) * Time.deltaTime : new Vector3(-Speed, 0f, 0f) * Time.deltaTime;
            }

            if (IsRotate)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,Speed,0f) * Time.deltaTime);
            }
        }
    }
}
