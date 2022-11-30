using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitileScene : MonoBehaviour
{
    void Start()
    {
        FadeManager.FadeIn(0.4f);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            FadeManager.FadeOut("AlphaScene_UI",0.4f);
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
}
