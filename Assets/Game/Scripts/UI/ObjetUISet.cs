using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjetUISet : MonoBehaviour
{
    [SerializeField]
    [Header("í≤ç∏UI")]
    private GameObject InvesUI;

    [SerializeField]
    [Header("ÉZÉäÉtUI")]
    private GameObject SerifUI;

    private bool display;

    InvestigationUI invesScript;
    SerifDisplay serifScript;

    void Start()
    {
        invesScript = InvesUI.GetComponent<InvestigationUI>();
        serifScript = SerifUI.GetComponent<SerifDisplay>();
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
            invesScript.SetBoolInvestigationDisplay(true);
            display = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Eda")
        {
            invesScript.SetBoolInvestigationDisplay(false);
            display = false;
        }
    }
}
