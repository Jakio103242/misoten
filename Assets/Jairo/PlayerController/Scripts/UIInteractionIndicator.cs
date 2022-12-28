using TMPro;
using UnityEngine;

public class UIInteractionIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName = default;

    public InteractableObject Current { get; private set; }

    private RectTransform targetCanvas;
    private CanvasGroup canvasGroup;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        targetCanvas = transform.root as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Set(InteractableObject interactable)
    {
        Current = interactable;
        gameObject.SetActive(Current != null);
        if (gameObject.activeSelf)
            itemName.text = Current.DisplayName;
    }

    private void Update()
    {
        canvasGroup.alpha = playerInput.IsCursorLocked ? 1 : 0;
        if (Current == null || Current.IsInteracting)
            return;
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(Current.transform.position);
        Vector2 WorldObject_ScreenPosition = new (
            ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));
        ((RectTransform)transform).anchoredPosition = WorldObject_ScreenPosition;
    }
}
