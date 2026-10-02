using System;
using System.Linq;
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
        get
        {
            if (instance == null) Logging.LogRegularStringMessage(
                $"it is vital that {nameof(GameEvents)} is attached to a GameObject", ErrorSeverity.Error);
            return instance;
        }
    }

    private int NrSubscribedDelegatesGameResumed
    {
        get => onGameResumed.GetInvocationList().Length;
    }

    public int NrSubscribedDelegatesRestart
    {
        get => onGameResumed.GetInvocationList().Length;
    }

    public int NrSubscribedDelegatesGamePause
    {
        get => onGamePaused.GetInvocationList().Length;
    }

    public bool ContainsDelegatePauseGame(Action function)
    {
        if (onGamePaused == null) return false;
        Delegate[] delegates = onGamePaused.GetInvocationList();
        return onGamePaused != null && delegates.Length > 0 && delegates.Contains(function);
    }

    public bool ContainsDelegateResumeGame(Action function)
    {
        if (onGameResumed == null) return false;
        Delegate[] delegates = onGameResumed.GetInvocationList();
        return delegates.Length > 0 && delegates.Contains(function);
    }

    public bool ContainsDelegateRestartGame(Action function)
    {
        if (onRestart == null) return false;
        Delegate[] delegates = onRestart.GetInvocationList();
        return onRestart != null && delegates.Length > 0 && delegates.Contains(function);
    }
    
    private void Awake()
    {
        CheckSingleton();
    }

    private void OnEnable()
    {
        CheckSingleton();
    }

    private void CheckSingleton()
    {
        // Fixes error when reloading a scene in editor where instance is null
        if (instance == null || instance == this) instance = this;
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
        onGameResumed?.Invoke();
    }
    
    public void PublishOnGameResumed()
    {
        onGameResumed?.Invoke();
    }
}