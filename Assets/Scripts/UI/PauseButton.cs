using UnityEngine;
using System;
using UnityEngine.UI;
using Utility;

[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Image pauseButtonImage;
    [Tooltip("Reference can be left null. If null image wont change when button pressed!")]
    [SerializeField] private PauseImages images;

    private void Awake()
    {
        CheckReferences();
    }

    private void Start()
    {
        SubscribeButtonEvents();
        SubscribeEvents();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref pauseButton, gameObject, nameof(pauseButton));
        ReferenceValidator.CheckComponentForNull(ref pauseButtonImage, gameObject, nameof(pauseButtonImage));
        ReferenceValidator.CheckUnityObjectForNull(ref images, nameof(images), ErrorSeverity.None);
    }

    private void SubscribeButtonEvents()
    {
        if (pauseButton != null) pauseButton.onClick.AddListener(GameFunctions.SwitchPauseResume);
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
        GameEvents.Instance.onGamePaused += OnPause;
        GameEvents.Instance.onGameResumed += OnResume;
    }
}