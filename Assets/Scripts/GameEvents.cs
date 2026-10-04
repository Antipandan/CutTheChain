using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Utility;
using Logging = Utility.Logging;
using Object = UnityEngine.Object;

public sealed class GameEvents : MonoBehaviour
{
    private static GameEvents instance;

    public Action onStaticAwake;
    
    public Action onGamePaused;

    public Action onGameResumed;

    public Action onRestart;

    public static GameEvents Instance
    {
        get
        {
            if (instance == null) Logging.LogRegularStringMessage(
                $"it is vital that {nameof(GameEvents)} is attached to a GameObject", ErrorSeverity.ScenePivotal);
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

    private void SubscribeStaticEvents()
    {
        IStaticAwake[] staticAwakeInstances = FindAllStaticAwakes();
        for (int i = 0; i < staticAwakeInstances.Length; i++)
        {
            IStaticAwake staticAwake = staticAwakeInstances[i];
            onStaticAwake += staticAwake.StaticAwake;
        }
    }

    public void PublishAllStaticEvents()
    {
        if (onStaticAwake == null)
        {
            Logging.LogRegularStringMessage($"event {nameof(onStaticAwake)} was null",  ErrorSeverity.Warning, gameObject);
            return;
        }
        Delegate[] delegates = onStaticAwake.GetInvocationList();
        if (delegates.Length <= 0)
        {
            Logging.LogRegularStringMessage("no delegates to publish", ErrorSeverity.Warning, gameObject);
            return;
        }
        for (int i = 0; i < onStaticAwake!.GetInvocationList().Length; i++)
        {
            onStaticAwake?.Invoke();
        }
    }

    private static IStaticAwake[] FindAllStaticAwakes()
    {
        List<IStaticAwake> results = new List<IStaticAwake>();
        // this is kinda ugly but works
        if (FindAnyObjectByType<MonoBehaviour>().gameObject.TryGetComponent<IStaticAwake>(out IStaticAwake staticAwake))
        {
            results.Add(staticAwake);
        }
        return results.ToArray();
    }
    
    private void Awake()
    {
        CheckSingleton();
        // bad usage of events replace with direct method calls!
        SubscribeStaticEvents();
        PublishAllStaticEvents();
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
    
    public void PublishOnStaticAwake()
    {
        onStaticAwake?.Invoke();
    }
}