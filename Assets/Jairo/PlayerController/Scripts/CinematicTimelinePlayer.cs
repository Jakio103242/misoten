using System;
using System.Collections;
using Fungus;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Flowchart))]
public class CinematicTimelinePlayer : MonoBehaviour
{
    public event Action StatusUpdated;
    public event Action<CinematicPart> PartUnlocked;
    
    [SerializeField] private PlayableDirector director = default;
    [SerializeField] private CinematicPart[] parts = default;
    [SerializeField] private float playbackSpeed = 1.0F;

    private bool isPartSet;
    private Flowchart flowchart;
    private CinematicPart current;

    public CinematicPart Current
    {
        get
        {
            if (!isPartSet)
                return null;
            return current;
        }
        private set
        {
            isPartSet = value != null;
            current = value;
        }
    }

    public bool IsPaused { get; private set; }
    public CinematicPart[] Parts => parts;
    public float CurrentProgress => Current == null ? 1 : (float)((director.time - Current.Start) / (Current.End - Current.Start));
    public bool RewindInput { get; set; }
    public bool FastForwardInput { get; set; }
    
    public void Awake()
    {
        flowchart = GetComponent<Flowchart>();
        director.timeUpdateMode = DirectorUpdateMode.Manual;
        director.time = 0;
        director.Evaluate();
        Current = null;
    }

    private void Update()
    {
        director.gameObject.SetActive(Current != null);
        if (!director.gameObject.activeSelf || Current == null)
            return;
        if (Current == null) 
            return;
        
        if (IsPaused)
        {
            if (FastForwardInput)
                SetDirectorTime(director.time + Time.deltaTime * playbackSpeed);
            else if (RewindInput)
                SetDirectorTime(director.time - Time.deltaTime * playbackSpeed);
        }
        else
        {
            SetDirectorTime(director.time + Time.deltaTime * playbackSpeed);
        }
    }

    private void SetDirectorTime(double newTime)
    {
        director.time = newTime;

        if (Current != null)
        {
            if (director.time < Current.Start)
            {
                director.time = Current.Start;
            }
            else if (director.time > Current.End)
            {
                if (IsPaused)
                    director.time = Current.End;
                else
                    StopCinematic();
            }
        }
        
        if (Current != null)
            director.Evaluate();
    }

    public void StopCinematic()
    {
        IsPaused = false;
        Current = null;
        StatusUpdated?.Invoke();
    }

    public void UnlockCinematic(string id)
    {
        CinematicPart unlockPart = Array.Find(parts, p => p.Id == id);
        if (unlockPart != null)
        {
            unlockPart.IncreaseVariable();
            if (unlockPart.IsUnlocked)
            {
                if (Current != null)
                    StopCinematic();
                Block b = flowchart.FindBlock($"Unlocked_{unlockPart.Id}");
                if (b != null)
                    StartCoroutine(UnlockAndPlay(b, unlockPart));
                else
                    PlayCinematic(unlockPart.Id);
                PartUnlocked?.Invoke(unlockPart);
            }
            StatusUpdated?.Invoke();
        }
        else
            Debug.LogError($"Cinematic with id {id} not found for unlock.");
    }

    private IEnumerator UnlockAndPlay(Block b, CinematicPart part)
    {
        yield return b.Execute();
        //PlayCinematic(part.Id);
    }

    public void PlayCinematic(string id)
    {
        CinematicPart cinematic = Array.Find(parts, p => p.Id == id);

        if (cinematic != null)
        {
            if (!cinematic.IsUnlocked)
                return;
            Current = cinematic;
            SetDirectorTime(cinematic.Start);
            IsPaused = false;
            StatusUpdated?.Invoke();
            return;
        }
        Debug.LogError($"Cinematic with id {id} not fount for play.");
    }

    public void TogglePause()
    {
        if (Current == null)
        {
            IsPaused = false;
            return;
        }
        IsPaused = !IsPaused;
        StatusUpdated?.Invoke();
    }
}
