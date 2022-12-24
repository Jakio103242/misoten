using UnityEngine;
using UnityEngine.InputSystem;
using Game.Player;

[RequireComponent(typeof(PlayerController), typeof(PlayerCamera))]
public class PlayerInput : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference lookInput;
    [Header("Parameters")]
    [SerializeField] private bool lockCursor = true;
    [SerializeField] private bool lockGameplayWhenCursorIsVisible = true;
    
    private PlayerController playerController;
    private PlayerCamera playerCamera;
    private InputAction movementCache;
    private InputAction lookCache;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerCamera = GetComponent<PlayerCamera>();
    }

    private void Update()
    {
        if (Keyboard.current[Key.F1].wasPressedThisFrame)
            lockCursor = !lockCursor;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
        if (lockGameplayWhenCursorIsVisible && Cursor.visible)
            return;
        playerController.MovementInput = GetMovementInput();
        playerCamera.Input = GetLookInput();
    }
    
    private Vector2 GetMovementInput()
    {
        if (movementCache != null)
            return movementCache.ReadValue<Vector2>();
        movementCache = movementInput.action;
        movementCache.Enable();
        return movementCache.ReadValue<Vector2>();
    }
    
    private Vector2 GetLookInput()
    {
        if (lookCache != null)
            return lookCache.ReadValue<Vector2>();
        lookCache = lookInput.action;
        lookCache.Enable();
        return lookCache.ReadValue<Vector2>();
    }
}