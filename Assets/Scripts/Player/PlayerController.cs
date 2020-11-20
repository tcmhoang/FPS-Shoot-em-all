using System;
using System.Collections.Generic;
using Assets.Scripts.Elements;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

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
        private bool _isShiftHold;

        private Transform _firePoint;

        private float _maxDistance;

        public Gun ActiveGun;

        public List<Gun> Guns = new List<Gun>();
        public List<Gun> UnlockableGuns = new List<Gun>();
        private int _curGun;

        public Transform AdsPoint, GunHolder;
        private Vector3 _gunStartPosition;
        private const float ADS_SPEED = 2;

        public Transform MuzzleFlash;

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _baseGravity = Physics.gravity.y * GravityModifier * Time.deltaTime; // acceleration
            _characterController = GetComponent<CharacterController>();
            _maxDistance = BulletController.LifeTime / Time.deltaTime * BulletController.Speed;

            _curGun = 0;
            SetGun();

            _gunStartPosition = GunHolder.localPosition;
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

            MuzzleFlash.gameObject.SetActive(false);

            SwitchGun();

            Aim();

            if (ActiveGun.CanFireNow)
            {
                if (Input.GetMouseButtonDown(0))
                    Shoot();

                AutoFire();
            }

            Animate();
        }

        private void Aim()
        {
            if (Input.GetMouseButtonDown(1))
                CameraController.Instance.ZoomIn(ActiveGun.ZoomAmount);

            if (Input.GetMouseButtonUp(1))
                CameraController.Instance.ZoomOut();

            if (Input.GetMouseButton(1))
                GunHolder.position =
                    Vector3.MoveTowards(GunHolder.position, AdsPoint.position, ADS_SPEED * Time.deltaTime);
            else
                GunHolder.localPosition =
                    Vector3.MoveTowards(GunHolder.localPosition, _gunStartPosition, ADS_SPEED * Time.deltaTime);
        }

        private void AutoFire()
        {
            if (Input.GetMouseButton(0) && ActiveGun.CanAutoFire) Shoot();
        }

        private void Shoot()
        {
            ActiveGun.RemoveABullet();
            if (!ActiveGun.HasAmmo()) return;
            if (Physics.Raycast(CamTransform.position, CamTransform.forward, out var hit, _maxDistance) &&
                Vector3.Distance(hit.point, CamTransform.position) > 2f)
                _firePoint.LookAt(hit.point);
            else
                _firePoint.LookAt(CamTransform.position + _maxDistance * CamTransform.forward);
            Instantiate(ActiveGun.Bullet, _firePoint.position,
                _firePoint.rotation); //if use FirePoint then it will use size of obj

            MuzzleFlash.gameObject.SetActive(true);

            ActiveGun.Reload();
        }

        private void Animate()
        {
            Animator.SetFloat("MoveSpeed", _moveInput.magnitude);
            Animator.SetBool("OnGround", _canJump);
            Animator.SetBool("IsRunning", _isShiftHold);
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
            _isShiftHold = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            _moveInput *= _isShiftHold ? RunSpeed : MoveSpeed;
        }

        private void SetGravity(float currentYVelocity)
        {
            _moveInput.y = _baseGravity;
            _moveInput.y += _characterController.isGrounded ? 0 : currentYVelocity;
        }

        private void SetGun()
        {
            ActiveGun = Guns[_curGun];
            ActiveGun.gameObject.SetActive(true);
            _firePoint = ActiveGun.FirePoint;

            MuzzleFlash.localPosition = ActiveGun.FirePoint.localPosition;
        }

        private void SwitchGun()
        {
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            ActiveGun.gameObject.SetActive(false);

            _curGun++;
            _curGun = _curGun == Guns.Count ? 0 : _curGun;

            SetGun();
            ActiveGun.UpdateUi();
        }

        public void PickupGun(string name)
        {
            if(UnlockableGuns.Count == 0) return;
            var picked  = UnlockableGuns.Find(gun => string.Equals(gun.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (picked == null) return;
            UnlockableGuns.Remove(picked);
            Guns.Add(picked);

            _curGun = Guns.Count - 1;
            SetGun();
        }
    }
}