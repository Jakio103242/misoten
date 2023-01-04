using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObj;

    private bool menuAcrive = false;

    void Start()
    {
        menuAcrive = false;
    }

    void Update()
    {
        //tabキーでメニュー表示
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            menuAcrive = !menuAcrive;
        }

        if(menuAcrive == true)
        {
            menuObj.gameObject.SetActive(true);
        }
        else
        {
            menuObj.gameObject.SetActive(false);
        }
    }
}
