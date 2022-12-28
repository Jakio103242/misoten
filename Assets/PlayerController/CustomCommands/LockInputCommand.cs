using Fungus;
using UnityEngine;

[CommandInfo("Custom", "Lock Input", "Increase input lockers count.")]
[AddComponentMenu("")]
public class LockInputCommand : Command
{
    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    override public void OnEnter()
    {
        playerInput.Lockers += 1;
        Continue();
    }
}