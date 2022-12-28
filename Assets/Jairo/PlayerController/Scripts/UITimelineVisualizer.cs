using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UITimelineVisualizer : MonoBehaviour
{
    [SerializeField] private UICinematicPart partPrefab = default;
    [SerializeField] private RectTransform partContainer = default;
    [SerializeField] private GameObject[] selectIndicators = default;
    [SerializeField] private InputActionReference previousPartInput = default;
    [SerializeField] private InputActionReference nextPartInput = default;
    [SerializeField] private InputActionReference playPauseInput = default;
    [SerializeField] private InputActionReference rewindPartInput = default;
    [SerializeField] private InputActionReference fastForwardPartInput = default;

    private InputAction previousInputCache = default;
    private InputAction nextInputCache = default;
    private InputAction playPauseInputCache = default;
    private InputAction rewindPartInputCache = default;
    private InputAction fastForwardPartInputCache = default;
    private CinematicTimelinePlayer timelinePlayer = default;
    private int selectedPartIndex = 0;
    private readonly List<UICinematicPart> instancedParts = new ();
    private CanvasGroup cGroup;
    private PlayerInput playerInput;

    private UICinematicPart SelectedPart = default;

    private void Awake()
    {
        cGroup = gameObject.AddComponent<CanvasGroup>();
        playerInput = FindObjectOfType<PlayerInput>();
        timelinePlayer = FindObjectOfType<CinematicTimelinePlayer>();
        timelinePlayer.PartUnlocked += OnPartUnlocked;
        timelinePlayer.StatusUpdated += OnStatusUpdated;
        previousInputCache = previousPartInput.action;
        previousInputCache.performed += OnPreviousPerformed;
        nextInputCache = nextPartInput.action;
        nextInputCache.performed += OnNextPerformed;
        playPauseInputCache = playPauseInput.action;
        playPauseInputCache.performed += OnPlayPausePerformed;
        rewindPartInputCache = rewindPartInput.action;
        fastForwardPartInputCache = fastForwardPartInput.action;
        Array.ForEach(selectIndicators, i => i.SetActive(false));
        InstantiateParts();
        Select(0);
    }

    private void OnDestroy()
    {
        timelinePlayer.PartUnlocked -= OnPartUnlocked;
        timelinePlayer.StatusUpdated -= OnStatusUpdated;
        previousInputCache.performed -= OnPreviousPerformed;
        nextInputCache.performed -= OnNextPerformed;
        playPauseInputCache.performed -= OnPlayPausePerformed;
    }

    private void OnStatusUpdated() => instancedParts.ForEach(p => p.Refresh());

    private void OnPreviousPerformed(InputAction.CallbackContext obj) => Select(selectedPartIndex - 1);

    private void OnNextPerformed(InputAction.CallbackContext obj) => Select(selectedPartIndex + 1);

    private void OnPlayPausePerformed(InputAction.CallbackContext obj)
    {
        if (SelectedPart == null)
            return;
        if (timelinePlayer.Current == null)
            timelinePlayer.PlayCinematic(SelectedPart.ID);
        else
            timelinePlayer.TogglePause();
    }

    private void OnEnable()
    {
        previousInputCache.Enable();
        nextInputCache.Enable();
        playPauseInputCache.Enable();
        rewindPartInputCache.Enable();
        fastForwardPartInputCache.Enable();
    }

    private void OnDisable()
    {
        previousInputCache.Disable();
        nextInputCache.Disable();
        playPauseInputCache.Disable();
        rewindPartInputCache.Disable();
        fastForwardPartInputCache.Disable();
    }

    private void OnPartUnlocked(CinematicPart part) => SelectPart(part);

    private void Update()
    {
        cGroup.alpha = playerInput.IsCursorLocked ? 1 : 0;
        if (timelinePlayer.Current == null)
            return;
        timelinePlayer.FastForwardInput = fastForwardPartInputCache.IsPressed();
        timelinePlayer.RewindInput = rewindPartInputCache.IsPressed();
    }

    private void Select(int newIndex)
    {
        instancedParts.ForEach(p => p.SetSelected(false));
        List<UICinematicPart> unlockedParts = instancedParts.FindAll(p => p.IsUnlocked);
        if (unlockedParts.Count == 0)
            return;
        Array.ForEach(selectIndicators, i => i.SetActive(unlockedParts.Count > 1));
        string prevId = SelectedPart != null ? SelectedPart.ID : string.Empty;
        selectedPartIndex = newIndex;
        if (selectedPartIndex >= unlockedParts.Count)
            selectedPartIndex = 0;
        else if (selectedPartIndex < 0)
            selectedPartIndex = unlockedParts.Count - 1;
        SelectedPart = unlockedParts[selectedPartIndex];
        SelectedPart.SetSelected(true);
        if (prevId != SelectedPart.ID)
            timelinePlayer.StopCinematic();
    }

    public void SelectPart(CinematicPart part)
    {
        UICinematicPart unlockedPart = instancedParts.Find(p => p.Part == part);
        if (unlockedPart == null)
            return;
        List<UICinematicPart> unlockedParts = instancedParts.FindAll(p => p.IsUnlocked);
        Array.ForEach(selectIndicators, i => i.SetActive(unlockedParts.Count > 1));
        instancedParts.ForEach(p => p.SetSelected(false));
        SelectedPart = unlockedPart;
        SelectedPart.SetSelected(true);
    }

    private void InstantiateParts()
    {
        foreach (CinematicPart part in timelinePlayer.Parts)
        {
            UICinematicPart instance = Instantiate(partPrefab, partContainer, false);
            instance.SetUp(part, timelinePlayer);
            instancedParts.Add(instance);
        }
    }
}