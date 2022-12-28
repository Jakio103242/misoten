using Fungus;
using UnityEngine;

[RequireComponent(typeof(Flowchart))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string displayName = default;

    public string DisplayName => displayName;
    public bool IsInteracting => flowchart.HasExecutingBlocks();
    
    private Flowchart flowchart;

    private void Awake() => flowchart = GetComponent<Flowchart>();

    public void Interact()
    {
        if (!flowchart.ExecuteIfHasBlock("Interact"))
            Debug.LogWarning("Flowchart don't have a block called 'Interact'");
    }
}