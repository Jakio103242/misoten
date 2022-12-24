using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Game.Input;

namespace Game.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] InputReader input;
        [SerializeField] private bool lockCursor = true;
        [SerializeField] private bool lockGameplayWhenCursorIsVisible = true;

        private PlayerController playerController;
        private PlayerCamera playerCamera;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            playerCamera = GetComponent<PlayerCamera>();
            input.OnMove.Subscribe(value => OnMove(value)).AddTo(this);
            input.OnLook.Subscribe(value => OnLook(value)).AddTo(this);
        }

        private void Update()
        {
            if (Keyboard.current[Key.F1].wasPressedThisFrame)
                lockCursor = !lockCursor;
            Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCursor;
            if (lockGameplayWhenCursorIsVisible && Cursor.visible)
                return;
            // playerController.MovementInput = inputReader;
            // playerCamera.Input = GetLookInput();
        }

        private void OnMove(Vector2 value)
        {
            playerController.MovementInput = value;
        }

        private void OnLook(Vector2 value)
        {
            playerCamera.Input = value;
        }
    }
}
