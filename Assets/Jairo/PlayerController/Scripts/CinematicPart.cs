using System;
using Fungus;

[Serializable]
public class CinematicPart
{
    public string Id; 
    public float Start;
    public float End;
    public int Required;

    private VariableBase<int> Variable => FungusManager.Instance.GlobalVariables.GetOrAddVariable(Id, 0, typeof(IntegerVariable)); 
    private int Current => Variable.Value;

    public float Progress => Required == 0 ? 1 : Current / (float)Required;
    public bool IsUnlocked => Current >= Required;
    
    public void IncreaseVariable() => Variable.Value++;
}