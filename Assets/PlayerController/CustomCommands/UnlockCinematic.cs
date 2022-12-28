using Fungus;
using UnityEngine;

[CommandInfo("Custom", "Unlock Cinematic", "Unlock a cinematic")]
[AddComponentMenu("")]
public class UnlockCinematic : Command
{
    [SerializeField] private string id = default;

    private CinematicTimelinePlayer cinematicTimelinePlayer = default;
    private bool alreadyUsed = false;

    private void Awake()
    {
        cinematicTimelinePlayer = FindObjectOfType<CinematicTimelinePlayer>();
    }

    override public void Execute()
    {
        if (!alreadyUsed)
            cinematicTimelinePlayer.UnlockCinematic(id);
        alreadyUsed = true;
        Continue();
    }

    override public Color GetButtonColor() => alreadyUsed ? Color.grey : Color.cyan;
}