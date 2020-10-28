using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            transform.rotation = Target.rotation;
            transform.position = Target.position;
        }
    }
}
