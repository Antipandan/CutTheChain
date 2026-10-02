using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Utility;

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
       restartButton.onClick.AddListener(GameFunctions.ReloadScene);
    }
}