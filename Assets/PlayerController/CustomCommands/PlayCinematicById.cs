using Fungus;
using UnityEngine;

[CommandInfo("Custom", "Play Cinematic by ID", "Play cinematic by ID")]
[AddComponentMenu("")]
public class PlayCinematicById : Command
{
    [SerializeField] private string id = default;

    private CinematicTimelinePlayer cinematicTimelinePlayer = default;

    private void Awake()
    {
        cinematicTimelinePlayer = FindObjectOfType<CinematicTimelinePlayer>();
    }

    override public void Execute()
    {
        cinematicTimelinePlayer.PlayCinematic(id);
        Continue();
    }
}