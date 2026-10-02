using System;
using Unity.Android.Gradle;
using UnityEngine;
using Utility;
using Logging = Utility.Logging;

public sealed class GameEvents : MonoBehaviour
{
    private static GameEvents instance;
    
    public Action onGamePaused;

    public Action onGameResumed;

    public Action onRestart;

    public static GameEvents Instance
    {
        get => instance;
    }
    private void Awake()
    {
        CheckSingleton();
    }

    private void CheckSingleton()
    {
        if (instance == null) instance = this;
        else
        {
            Logging.LogSingletonError(nameof(instance), ErrorSeverity.Error);
            Destroy(gameObject);
        }
    }

    public void PublishOnGamePaused()
    {
        onGamePaused?.Invoke();
    }

    public void PublishOnRestart()
    {
        onRestart?.Invoke();
    }
    
    public void PublishOnGameResumed()
    {
        onGameResumed?.Invoke();
    }
}