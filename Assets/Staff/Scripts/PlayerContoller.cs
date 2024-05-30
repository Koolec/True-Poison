using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Borshevik.Manager;

namespace Borshevik.PlayerControl
{
    public class PlayerContoller : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private GameObject Weapon;
        [SerializeField] private Collider WeaponLine;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        [SerializeField, Range(10, 500)] private float JumpFactor = 260f;
        [SerializeField] private float Dis2Ground = 0.8f;
        [SerializeField] private LayerMask GroundCheck;
        [SerializeField] private float AirResistance = 0.8f;
        
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _grounded = false;
        private bool _weaponed = false;
        private bool _hasAnimator;
        private int _xVelHash;
        private int _yVelHash;
        private int _jumpHash;
        private int _groundHash;
        private int _fallingHash;
        private int _zVelHash;
        private int _crouchHash;
        private int _weapon0Hash;
        private int _weapon1Hash;
        private int _slashHash;
        private float _xRotation;
        public AudioSource audioSource;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;
        private Vector2 _currentVelocity;
        


        private void Start() {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();
            
            


            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _zVelHash = Animator.StringToHash("Z_Velocity");
            _jumpHash = Animator.StringToHash("Jump");
            _groundHash = Animator.StringToHash("Grounded");
            _fallingHash = Animator.StringToHash("Falling");
            _crouchHash = Animator.StringToHash("Crouch");

            _weapon0Hash = Animator.StringToHash("Weapon0");
            _weapon1Hash = Animator.StringToHash("Weapon1");

            _slashHash = Animator.StringToHash("Slash");
        }

        private void FixedUpdate() {
            SampleGround();
            Move();
            HandleJump();
            HandleCrouch();
            HandleWeapon();
            Slash();
        }
        private void LateUpdate() 
        {
            CamMovements();
        }

        private void Move()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
            audioSource.Play();
            audioSource = GetComponent<AudioSource>();
            }
            if(!_hasAnimator) return;

            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
            if(_inputManager.Crouch) targetSpeed = 1.5f;
            if(_inputManager.Move == Vector2.zero) targetSpeed = 0;
            

            if(_grounded)
            {
                
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y =  Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

                var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
                var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;

                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0 , zVelDifference)), ForceMode.VelocityChange);
            }
            else
            {
                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(_currentVelocity.x * AirResistance,0,_currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }


            _animator.SetFloat(_xVelHash , _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
            
        }

        private void CamMovements()
        {
            if(!_hasAnimator) return;

            var Mouse_X = _inputManager.Look.x;
            var Mouse_Y = _inputManager.Look.y;
            Camera.position = CameraRoot.position;
            
            
            _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0 , 0);
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
        }

        private void HandleCrouch() => _animator.SetBool(_crouchHash , _inputManager.Crouch);


        private void HandleJump()
        {
            if(!_hasAnimator) return;
            if(!_inputManager.Jump) return;
            if(!_grounded) return;
            _animator.SetTrigger(_jumpHash);
        }

        public void JumpAddForce()
        {
            _playerRigidbody.AddForce(-_playerRigidbody.velocity.y * Vector3.up, ForceMode.VelocityChange);
            _playerRigidbody.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
            _animator.ResetTrigger(_jumpHash);
        }

        private void SampleGround()
        {
            if(!_hasAnimator) return;
            
            RaycastHit hitInfo;
            if(Physics.Raycast(_playerRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, Dis2Ground + 0.1f, GroundCheck))
            {
                //Grounded
                _grounded = true;
                SetAnimationGrounding();
                return;
            }
            //Falling
            _grounded = false;
            _animator.SetFloat(_zVelHash, _playerRigidbody.velocity.y);
            SetAnimationGrounding();
            return;
        }

        private void SetAnimationGrounding()
        {
            _animator.SetBool(_fallingHash, !_grounded);
            _animator.SetBool(_groundHash, _grounded);
        }

        private void HandleWeapon()
        {
            if(!_hasAnimator) return;
            if(_inputManager.Weapon0) HandleWeapon0();
            if(_inputManager.Weapon1) HandleWeapon1();
        }

        private void HandleWeapon0()
        {
            _animator.ResetTrigger(_weapon1Hash);
            _animator.SetTrigger(_weapon0Hash);
            _weaponed = false;
            Weapon.SetActive(false);
        }
        private void HandleWeapon1()
        {
            _animator.SetTrigger(_weapon1Hash);
            _animator.ResetTrigger(_weapon0Hash);
            _weaponed = true;
            Weapon.SetActive(true);
        }

        private void Slash()
        {
            if(!_hasAnimator) return;
            if(!_weaponed) return;
            if(_inputManager.Slash)
            {
                _animator.SetTrigger(_slashHash);
                WeaponLine.GetComponent<CapsuleCollider>().enabled = true;
                return;
            }
            _animator.ResetTrigger(_slashHash);
            WeaponLine.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}