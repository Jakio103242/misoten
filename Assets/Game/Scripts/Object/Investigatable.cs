using UnityEngine;
using Data.Dialoue;

public class Investigatable : MonoBehaviour, IInvestigatable
{
    public DialogueData DialogueData;
    public bool HintActive;

    void Start()
    {
        HintActive = false;
    }
}