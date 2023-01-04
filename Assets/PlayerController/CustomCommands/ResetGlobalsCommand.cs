using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("Custom", "Reset Globals", "Clear all globals.")]
[AddComponentMenu("")]
public class ResetGlobalsCommand : Command
{
    override public void OnEnter()
    {
        foreach (KeyValuePair<string,Variable> variable in FungusManager.Instance.GlobalVariables.variables)
            variable.Value.OnReset();
        Continue();
    }
}