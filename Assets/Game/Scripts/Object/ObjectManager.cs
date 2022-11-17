using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Game.Input;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] List<Talkable> talkableObjects;
    [SerializeField] List<Investigatable> interactObjects;
    [SerializeField] float checkDistance;
    [SerializeField] Transform playerTransform;

    void Start()
    {
    }

    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            foreach(var interactObject in interactObjects)
            {
                if(interactObject.HintActive)
                {
                    Debug.Log(interactObject.DialogueData.Dialogue[0].name);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(input.GameInputs.Player.Move.inProgress) CheckVisibleCamera();
    }

    public void CheckVisibleCamera()
    {
        Vector3 viewportPos;
        float distance;
        for(int index = 0; index < interactObjects.Count; index++)
        {
            distance = (playerTransform.position - interactObjects[index].transform.position).sqrMagnitude;
            if(distance * distance > checkDistance * checkDistance)
            {
                interactObjects[index].HintActive = false;
                continue;
            }
            Debug.Log("a");
            viewportPos = Camera.main.WorldToViewportPoint(interactObjects[index].transform.position);
            if((viewportPos.x >= 0 && viewportPos.y >= 0) && (viewportPos.x <= 1 && viewportPos.y <= 1) && viewportPos.z >= 0)
            {
                interactObjects[index].HintActive = true;
            }
            else 
            {
                interactObjects[index].HintActive = false;
            }
        }
    }
}
