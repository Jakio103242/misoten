using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Data;
using Game.Intaract;
using Game.Story;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> speakerNames;
    [SerializeField] private List<TextMeshProUGUI> quotes;
    [SerializeField] private ObjectManager objectManager;

    private void Start()
    {
        
    }

    private void Update()
    {
        for(int index = 0; index < objectManager.Intaractables.Count; index++)
        {
            if(objectManager.Intaractables[index].HintActive)
            {
                //SetDialogue(index, objectManager.Intaractables[index].StoryIncident().DialogueData);
            }
            else
            {
                CleanDialogue(index);
            }
        }
    }

    void SetDialogue(int index, DialogueData dialogueData)
    {
        speakerNames[index].text = dialogueData.Dialogue[0].name;
        quotes[index].text = dialogueData.Dialogue[0].quote;
    }
    void CleanDialogue(int index)
    {
        speakerNames[index].text = "";
        quotes[index].text = "";
    }
}