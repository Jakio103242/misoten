using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitileScene : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private GameObject panel;

    void Start()
    {
        FadeManager.FadeIn(0.4f);
        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        FadeManager.FadeOut(nextSceneName, 0.4f);
    }

    public void EndGame()
    {
        button.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
    }

    public void PopUp_Yes()
    {
        Application.Quit();
    }

    public void PopUp_No()
    {
        button.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
    }

}
