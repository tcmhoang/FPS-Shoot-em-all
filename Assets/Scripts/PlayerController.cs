using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float MoveSpeed;

        public CharacterController CharacterController;

        public Transform CamTransform;

        private Vector3 _moveInput;

        public float MouseSensitivity;

        public bool InvertX;
        public bool InvertY;

        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            _moveInput.x = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            _moveInput.z = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

            CharacterController.Move(_moveInput);

            var mouseInVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * MouseSensitivity;

            if (InvertX) mouseInVector.x *= -1;
            if (InvertY) mouseInVector.y *= -1;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + mouseInVector.x, transform.rotation.eulerAngles.z);
            CamTransform.rotation = Quaternion.Euler(CamTransform.rotation.eulerAngles + new Vector3(- mouseInVector.y,0f,0f));
        }
    }
}
