using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;

        public Transform Target;

        private float _strFov, _targetFov;
        private const float ZOOM_SPEED = 7;
        private Camera _cam;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _cam = GetComponent<Camera>();

            _strFov = _cam.fieldOfView;
            _targetFov = _strFov;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            Rotate();

            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _targetFov, ZOOM_SPEED * Time.deltaTime);
        }

        private void Rotate()
        {
            transform.rotation = Target.rotation;
            transform.position = Target.position;
        }

        public void ZoomIn(float value)
        {
            _targetFov = value;
        }

        public void ZoomOut()
        {
            _targetFov = _strFov;
        }
    }
}