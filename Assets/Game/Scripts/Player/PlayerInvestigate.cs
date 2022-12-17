using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Input;
using Game.Intaract;
using Game.Story;

public class PlayerInvestigate : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] ObjectManager objectManager;

    void Start()
    {
        // input
    }

    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Intaractable investigatable = objectManager.NearestIntaractableObject();
            if(investigatable != null)
            {
                //dialogueManager.SetDialogue(((StoryInvestigationEvent)investigatableObject.StoryIncident()).DialogueData);
                investigatable.Completed = true;
            }
        }
    }
}
