using System;
using Fungus;
using UnityEngine;

[CommandInfo("Custom", "Unlock Input", "Decrease input lockers count.")]
[AddComponentMenu("")]
public class UnlockInputCommand : Command
{
    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    override public void OnEnter()
    {
        playerInput.Lockers -= 1;
        Continue();
    }
}