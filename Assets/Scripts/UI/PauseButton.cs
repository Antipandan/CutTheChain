using UnityEngine;
using System;
using UnityEngine.UI;
using CustomUtility;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Image pauseButtonImage;
    [Tooltip("Reference can be left null. If null image wont change when button pressed!")]
    [SerializeField] private PauseImages images;
    private bool isPaused = false;

    public bool IsPaused
    {
        get => isPaused;
    }

    private void Awake()
    {
        CheckReferences();
    }
    
    private void OnEnable()
    {
        if (GameEvents.Instance == null) return;
        SubscribeAllEvents();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref pauseButton, gameObject, nameof(pauseButton));
        ReferenceValidator.CheckComponentForNull(ref pauseButtonImage, gameObject, nameof(pauseButtonImage));
#pragma warning disable CS0219 // Variable is assigned but its value is never used
        bool log = false;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
        ReferenceValidator.CheckUnityObjectForNull(ref images, nameof(images), ErrorSeverity.None);
    }

    private void SubscribeButtonEvents()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveListener(GameFunctions.SwitchPauseResume);
            pauseButton.onClick.AddListener(GameFunctions.SwitchPauseResume);
        }
    }

    private void SubscribeAllEvents()
    {
        SubscribeButtonEvents();
        SubscribeEvents();
    }

    private void UNSubscribeAllEvents()
    {
        UnSubscribeButtonEvents();
        UnSubscribeEvents();
    }

    private void OnPause()
    {
        if (pauseButtonImage == null || images == null) return;
        if (images.PauseImage != null) pauseButtonImage.sprite = images.PauseImage;
    }

    private void OnResume()
    {
        if (pauseButtonImage == null || images == null) return;
        if (images.RestartImage != null) pauseButtonImage.sprite = images.RestartImage;
    }
    
    private void SubscribeEvents()
    {
        GameEvents.Instance.onGameResumed -= OnResume;
        GameEvents.Instance.onGameResumed += OnResume;
        GameEvents.Instance.onGamePaused -= OnPause;
        GameEvents.Instance.onGamePaused += OnPause;
    }

    private void UnSubscribeButtonEvents()
    {
        if (pauseButton != null) pauseButton.onClick.RemoveListener(GameFunctions.SwitchPauseResume);
    }

    private void UnSubscribeEvents()
    {
        GameEvents.Instance.onGamePaused -= OnPause;
        GameEvents.Instance.onGameResumed -= OnResume;
    }

    private void OnDisable()
    {
        UNSubscribeAllEvents();
    }
}