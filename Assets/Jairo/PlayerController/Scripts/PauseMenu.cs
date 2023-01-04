using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private CanvasGroup cGroup;
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        cGroup = gameObject.AddComponent<CanvasGroup>();
        Refresh();
    }

    private void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current[GamepadButton.Start].wasReleasedThisFrame)
                TogglePause();
            if (cGroup.interactable && Gamepad.current[GamepadButton.Select].wasPressedThisFrame)
                Menu();
        }
        
        if (Keyboard.current != null && Keyboard.current[Key.Escape].wasReleasedThisFrame)
            TogglePause();
    }

    private void TogglePause()
    {
        IsPaused = !IsPaused;
        Refresh();
    }

    private void Refresh()
    {
        Time.timeScale = IsPaused ? 0 : 1;
        cGroup.alpha = IsPaused ? 1 : 0;
        cGroup.blocksRaycasts = IsPaused;
        cGroup.interactable = IsPaused;
    }

    private void OnDestroy()
    {
        IsPaused = false;
        Refresh();
    }

    private void OnDisable()
    {
        IsPaused = false;
        Refresh();
    }

    public void Resume()
    {
        IsPaused = false;
        Refresh();
    }

    public void Menu()
    {
        cGroup.blocksRaycasts = false;
        cGroup.interactable = false;
        SceneManager.LoadScene(0);
    }
}
