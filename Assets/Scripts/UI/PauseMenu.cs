using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private PercentageHandler percentageVolume;
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        CheckReferences();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref backgroundImage, gameObject, nameof(backgroundImage));
        ReferenceValidator.CheckComponentForNull(ref musicSlider, gameObject, nameof(musicSlider));
        ReferenceValidator.CheckComponentForNull(ref musicText, gameObject, nameof(musicText));
        ReferenceValidator.CheckComponentForNull(ref percentageVolume, gameObject, nameof(percentageVolume));
        ReferenceValidator.CheckComponentForNull(ref gameStatusText, gameObject, nameof(gameStatusText));
        ReferenceValidator.CheckComponentForNull(ref ResumeButton, gameObject, nameof(ResumeButton));
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }
    
    private void SubscribeEvents()
    {
        // this is apparently safe and good to do in c#? https://stackoverflow.com/questions/367523/how-to-ensure-an-event-is-only-subscribed-to-once
        // check comment for further discussion
        GameEvents.Instance.onGamePaused -= OnPause;
        GameEvents.Instance.onGamePaused += OnPause;
        GameEvents.Instance.onGameResumed -= OnResume;
        GameEvents.Instance.onGameResumed += OnResume;
    }

    private void UnSubscribeEvents()
    {
        GameEvents.Instance.onGamePaused -= OnPause;
        GameEvents.Instance.onGameResumed -= OnResume;
    }
    
    private void OnPause()
    {
        gameObject.SetActive(true);
    }

    private void OnResume()
    {
        gameObject.SetActive(false);
    }
    
}