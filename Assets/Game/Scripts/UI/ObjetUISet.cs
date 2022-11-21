using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetUISet : MonoBehaviour
{
    [SerializeField]
    [Header("í≤ç∏UI")]
    private GameObject InvesUI;

    [SerializeField]
    [Header("ÉZÉäÉtUI")]
    private GameObject SerifUI;

    InvestigationUI invesScript;
    SerifDisplay serifScript;

    void Start()
    {
        invesScript = InvesUI.GetComponent<InvestigationUI>();
        serifScript = SerifUI.GetComponent<SerifDisplay>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        invesScript.SetBoolInvestigationDisplay(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        invesScript.SetBoolInvestigationDisplay(false);
    }
}
