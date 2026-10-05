using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using CustomUtility;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        CheckReferences();
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref restartButton, gameObject, nameof(restartButton));
    }

    private void Start()
    {
        SubscribeButtonEvents();
    }

    private void SubscribeButtonEvents()
    {
        restartButton.onClick.RemoveListener(Reload);
        restartButton.onClick.AddListener(Reload);
    }

    private static void Reload()
    {
        GameFunctions.ReloadScene();
        GameFunctions.ResumeGame();
    }
    
    private void UnSubscribeButtonEvents()
    {
        restartButton.onClick.RemoveListener(Reload);
    }

    private void OnDestroy()
    {
        UnSubscribeButtonEvents();
    }
}