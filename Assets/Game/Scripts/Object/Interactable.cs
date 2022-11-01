using UnityEngine;
using Data.Dialoue;

public class Interactable : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueData;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Action()
    {
        dialogueManager.StartDialogue(dialogueData);
    }
}