using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Borshevik.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;

        public Vector2 Move {get; private set;}
        public Vector2 Look {get; private set;}
        public bool Run {get; private set;}
        public bool Jump {get; private set;}
        public bool Crouch {get; private set;}

        public bool Weapon0 {get; private set;}
        public bool Weapon1 {get; private set;}

        public bool Slash {get; private set;}
        

        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;

        private InputAction _weapon0Action;
        private InputAction _weapon1Action;

        private InputAction _slashAction;

        private void Awake() {
            
           
            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction  = _currentMap.FindAction("Run");
            _jumpAction = _currentMap.FindAction("Jump");
            _crouchAction = _currentMap.FindAction("Crouch");

            _weapon0Action = _currentMap.FindAction("Weapon0");
            _weapon1Action = _currentMap.FindAction("Weapon1");

            _slashAction = _currentMap.FindAction("Slash");


            _moveAction.performed +=OnMove;
            _lookAction.performed += OnLook;
            _runAction.performed += OnRun;
            _jumpAction.performed += OnJump;
            _crouchAction.started += OnCrouch;

            _weapon0Action.started += OnWeapon0;
            _weapon1Action.started += OnWeapon1;

            _slashAction.started += OnSlash;


            _moveAction.canceled += OnMove;
            _lookAction.canceled += OnLook;
            _runAction.canceled += OnRun;
            _jumpAction.canceled += OnJump;
            _crouchAction.canceled += OnCrouch;

            _weapon0Action.canceled += OnWeapon0;
            _weapon1Action.canceled += OnWeapon1;

            _slashAction.canceled += OnSlash;
        }
        public void Start(){
            
            
        }

       

        private void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
            
            
        }
        private void OnLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }
        private void OnRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
            
        }
        private void OnJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
        }
        private void OnCrouch(InputAction.CallbackContext context)
        {
            Crouch = context.ReadValueAsButton();
        }

        private void OnWeapon0(InputAction.CallbackContext context)
        {
            Weapon0 = context.ReadValueAsButton();
        }

        private void OnWeapon1(InputAction.CallbackContext context)
        {
            Weapon1 = context.ReadValueAsButton();
        }

        private void OnSlash(InputAction.CallbackContext context)
        {
            Slash = context.ReadValueAsButton();
        }

        private void OnEnable() 
        {
            _currentMap.Enable();
        }

        private void OnDisable() 
        {
            _currentMap.Disable();
        }
        
    }
}
