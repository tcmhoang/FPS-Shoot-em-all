using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float MoveSpeed, GravityModifier, JumpPower, RunSpeed = 12f;

        private float _baseGravity;

        private CharacterController _characterController;

        public Transform CamTransform;

        private Vector3 _moveInput;

        public float MouseSensitivity;

        public bool InvertX;
        public bool InvertY;

        public Transform GroundCheckPoint;
        public LayerMask GroundRadar;
        private bool _canJump;
        private bool _canJumpAgain;

        public Animator Animator;

        // Start is called before the first frame update
        private void Start()
        {
            _baseGravity = Physics.gravity.y * GravityModifier * Time.deltaTime; // acceleration
            _characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame


        private void Update()
        {
            // _moveInput.x = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            // _moveInput.z = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
            var currentYVelocity = _moveInput.y;

            SetMoveVector();

            SetGravity(currentYVelocity);

            Jump();

            _characterController.Move(_moveInput * Time.deltaTime);

            RotateCameraByCursor();

            Animate();
        }

        private void Animate()
        {
            Animator.SetFloat("MoveSpeed", _moveInput.magnitude);
            Animator.SetBool("OnGround",_canJump);
        }

        private void Jump()
        {
            _canJump = Physics.OverlapSphere(GroundCheckPoint.position, .25f, GroundRadar).Length > 0;
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            if (_canJump)
            {
                _moveInput.y = JumpPower;
                _canJumpAgain = true;
            }
            else if (_canJumpAgain)
            {
                _moveInput.y = JumpPower;
                _canJumpAgain = false;
            }
        }


        private void RotateCameraByCursor()
        {
            var mouseInVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) *
                                MouseSensitivity;
            if (InvertX) mouseInVector.x *= -1;
            if (InvertY) mouseInVector.y *= -1;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + mouseInVector.x, transform.rotation.eulerAngles.z);
            CamTransform.rotation =
                Quaternion.Euler(CamTransform.rotation.eulerAngles + new Vector3(-mouseInVector.y, 0f, 0f));
        }

        private void SetMoveVector()
        {
            var verticalMove = transform.forward * Input.GetAxis("Vertical");
            var horizontalMove = transform.right * Input.GetAxis("Horizontal");
            _moveInput = (verticalMove + horizontalMove).normalized;
            _moveInput *= Input.GetKey(KeyCode.LeftShift) ? RunSpeed : MoveSpeed;
        }

        private void SetGravity(float currentYVelocity)
        {
            _moveInput.y = _baseGravity;
            _moveInput.y += _characterController.isGrounded ? 0 : currentYVelocity;
        }
    }
}