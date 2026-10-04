using UnityEngine;
using System;
using TMPro;
using UnityEngine.UIElements;
using Utility;

public class GameStatusMenu : MonoBehaviour
{
    [SerializeField] private CanvasRenderer gameStatusMenuObject;
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private UnityEngine.UI.Image backGroundImage;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] private UnityEngine.UI.Button restartButton;
    [SerializeField] private TextMeshProUGUI exitMainMenuText;
    [SerializeField] private UnityEngine.UI.Button exitMainMenuButton;
    [SerializeField] private TextMeshProUGUI exitGameButtonText;
    [SerializeField] private UnityEngine.UI.Button exitGameButton;

    private void Awake()
    {
        CheckReferences();
        DisableMenu();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref gameStatusMenuObject, gameObject, nameof(gameStatusMenuObject));
        ReferenceValidator.CheckComponentForNull(ref gameStatusText, gameObject, nameof(gameStatusText));
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
        ReferenceValidator.CheckComponentForNull(ref restartText, gameObject, nameof(restartText));
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
        ReferenceValidator.CheckComponentForNull(ref exitMainMenuText, gameObject, nameof(exitMainMenuText));
        ReferenceValidator.CheckComponentForNull(ref exitGameButtonText, gameObject, nameof(exitGameButtonText));
        ReferenceValidator.CheckComponentForNull(ref exitGameButton, gameObject, nameof(exitGameButton));
    }

    private void SubscribeToEvents()
    {
        GameManager.Instance.OnFruitEaten -= OnGameOver;  
        GameManager.Instance.OnFruitEaten += OnGameOver;
        restartButton.onClick?.RemoveListener(GameFunctions.ReloadScene);
        restartButton.onClick?.AddListener(GameFunctions.ReloadScene);
        exitGameButton.onClick.RemoveListener(GameFunctions.ExitGame);
        exitGameButton.onClick?.AddListener(GameFunctions.ExitGame);
        exitMainMenuButton.onClick?.RemoveListener(GameFunctions.LoadMainMenuScene);
        exitMainMenuButton.onClick?.AddListener(GameFunctions.LoadMainMenuScene);
    }

    private void UnSubscribeEvents()
    {
        restartButton.onClick?.RemoveListener(GameFunctions.ReloadScene);
        exitMainMenuButton.onClick?.RemoveListener(GameFunctions.LoadMainMenuScene);
        exitGameButton.onClick?.RemoveListener(GameFunctions.ExitGame);
        GameManager.Instance.OnFruitEaten -= OnGameOver;  
    }

    private void DisableMenu()
    {
        gameStatusMenuObject.gameObject.SetActive(false);
    }

    private void EnableMenu()
    {
        gameStatusMenuObject.gameObject.SetActive(true);
    }

    private void OnGameOver()
    {
        EnableMenu();
    }
}