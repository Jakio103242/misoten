using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitileScene : MonoBehaviour
{
    void Start()
    {
        FadeManager.FadeIn();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            FadeManager.FadeOut("AlphaScene_UI");
        }

    }
}
