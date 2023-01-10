using UnityEngine;
using UniRx;
using Game.Input;
using Game.Intaract;
using Game.UI;

public class PlayerInvestigate : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private InvestigationUI investigationUI;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private ObjectManager objectManager;

    private void Start()
    {
        input.OnInvestigate.Subscribe(_ => OnInvestigate()).AddTo(this);
    }

    private void OnInvestigate()
    {
        Intaractable intaractableObject = objectManager.NearestIntaractableObject();
        if(intaractableObject != null)
        {
            intaractableObject.Completed = true;
            if(intaractableObject.GetType() == typeof(Investigatable))
            {
                investigationUI.OnDisplay(((Investigatable)intaractableObject).GetInvestigationData());
            }
            if(intaractableObject.GetType() == typeof(Talkable))
            {
                dialogueUI.OnDisplay(((Talkable)intaractableObject).GetDialogueData());
            }
            return;
        }
        investigationUI.NonDisplay();
        dialogueUI.NonDisplay();
    }
}
