using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.UI;

public class ObjetUISet : MonoBehaviour
{
    [SerializeField]
    [Header("InvesUI")]
    private GameObject InvesUI;

    [SerializeField]
    [Header("SerifUI")]
    private GameObject SerifUI;

    private bool display;

    InvestigationUI invesScript;
    DialogueUI serifScript;

    void Start()
    {
        invesScript = InvesUI.GetComponent<InvestigationUI>();
        serifScript = SerifUI.GetComponent<DialogueUI>();
        display = false;
    }

    private void Update()
    {
        if (display == true && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            serifScript.SetBoolSerifDisplay(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Eda")
        {
            //invesScript.SetBoolInvestigationDisplay(true);
            display = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Eda")
        {
            // invesScript.SetBoolInvestigationDisplay(false);
            display = false;
        }
    }
}
