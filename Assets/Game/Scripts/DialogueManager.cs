using UnityEngine;
using TMPro;
using Data.Dialoue;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] new private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI quote;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void StartDialogue(DialogueData dialogueData)
    {
        
        name.text = dialogueData.Dialogue[0].name;
        quote.text = dialogueData.Dialogue[0].quote;
    }
}