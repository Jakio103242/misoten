using Phasmagoria.Player;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController), typeof(PlayerCamera))]
public class PlayerInput : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference lookInput;
    [SerializeField] private InputActionReference interactInput;
    [Header("Parameters")]
    [SerializeField] private bool lockCursor = true;
    [SerializeField] private bool lockGameplayWhenCursorIsVisible = true;

    private PauseMenu pauseMenu;
    private PlayerController playerController;
    private InteractionSystem interactionSystem;
    private PlayerCamera playerCamera;
    private InputAction movementCache;
    private InputAction lookCache;
    private InputAction interactCache;
    private bool isForcedCursor;
    
    public int Lockers { get; set; }

    public bool IsCursorLocked => lockCursor && Lockers <= 0;

    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        playerController = GetComponent<PlayerController>();
        interactionSystem = GetComponent<InteractionSystem>();
        playerCamera = GetComponent<PlayerCamera>();
        interactCache = interactInput.action;
        interactCache.performed += OnInteractPerformed;
        interactCache.Enable();
    }

    private void OnDestroy()
    {
        interactCache.performed -= OnInteractPerformed;
    }

    private void OnInteractPerformed(InputAction.CallbackContext action)
    {
        if (lockCursor && interactionSystem.Current != null && !interactionSystem.Current.IsInteracting)
            interactionSystem.Current.Interact();
    }

    private void Update()
    {
        Lockers = Mathf.Max(0, Lockers);
        if (Keyboard.current[Key.F1].wasPressedThisFrame)
            isForcedCursor = !isForcedCursor;

        lockCursor = !Fungus.SayDialog.GetSayDialog().gameObject.activeSelf;
        lockCursor &= !Fungus.MenuDialog.GetMenuDialog().gameObject.activeSelf;

        if (pauseMenu != null && pauseMenu.IsPaused)
            lockCursor = false;
        if (isForcedCursor)
            lockCursor = false;
        
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
        if (Lockers > 0 || (lockGameplayWhenCursorIsVisible && Cursor.visible))
        {
            playerController.MovementInput = Vector2.zero;
            playerCamera.Input = Vector2.zero;
            return;
        }
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