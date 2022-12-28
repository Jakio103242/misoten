using UnityEngine;
using UnityEngine.UI;

public class UICinematicPart : MonoBehaviour
{
    [Header("Progress")]
    [SerializeField] private Image progressImage = default;
    [Header("Selection")]
    [SerializeField] private Image selectIndicator = default;
    [Header("Status")]
    [SerializeField] private Image lockImage = default;
    [SerializeField] private Image playImage = default;
    [SerializeField] private Image pauseImage = default;
    [SerializeField] private Image rewindImage = default;
    [SerializeField] private Image fastForwardImage = default;
    [Header("Controls")]
    [SerializeField] private Image playPauseControl = default;
    [SerializeField] private Image rewindControl = default;
    [SerializeField] private Image fastForwardControl = default;

    private CinematicPart myPart = default;
    private CinematicTimelinePlayer player;
    private bool isSelected = false;

    public string ID => myPart.Id;
    public bool IsUnlocked => myPart.IsUnlocked;
    public CinematicPart Part => myPart;
    
    private void Awake()
    {
        selectIndicator.enabled = false;
        lockImage.enabled = false;
        playImage.enabled = false;
        pauseImage.enabled = false;
        rewindImage.enabled = false;
        fastForwardImage.enabled = false;
        playPauseControl.enabled = false;
        rewindControl.enabled = false;
        fastForwardControl.enabled = false;
        progressImage.color = new Color(1, 1, 1, 0.5F);
    }

    public void SetUp(CinematicPart part, CinematicTimelinePlayer timelinePlayer)
    {
        myPart = part;
        player = timelinePlayer;
        Refresh();
    }

    private void Update()
    {
        if (player == null || myPart == null)
            return;
        string id = player.Current == null ? string.Empty : player.Current.Id;
        progressImage.color = id == myPart.Id || isSelected ? Color.white : new Color(1, 1, 1, 0.5F);
        progressImage.fillAmount = id == myPart.Id || isSelected ? player.CurrentProgress : myPart.Progress;
    }

    public void Refresh()
    {
        if (player == null || myPart == null)
            return;
        selectIndicator.enabled = isSelected;
        playImage.enabled = isSelected && (player.Current == null || player.IsPaused);
        pauseImage.enabled = isSelected && (player.Current != null && !player.IsPaused);
        rewindImage.enabled = isSelected && (player.Current != null && player.IsPaused);
        fastForwardImage.enabled = isSelected && (player.Current != null && player.IsPaused);
        rewindControl.enabled = isSelected && (player.Current != null && player.IsPaused);
        fastForwardControl.enabled = isSelected && (player.Current != null && player.IsPaused);
        playPauseControl.enabled = isSelected;
        lockImage.enabled = !myPart.IsUnlocked;
    }

    public void SetSelected(bool selected)
    {
        isSelected = myPart.IsUnlocked && selected;
        Refresh();
    }
}