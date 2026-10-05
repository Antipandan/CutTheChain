using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using CustomUtility;
using JetBrains.Annotations;
using Logging = CustomUtility.Logging;
using Object = UnityEngine.Object;

public sealed class GameEvents : MonoBehaviour
{
    private static GameEvents instance;

    public Action onStaticAwake;

    public Action<RopeConnectors> onRopeCut;
    
    public Action onGamePaused;

    public Action onGameResumed;

    public Action onRestart;


    #region Properties
    
    [CanBeNull]
    public static GameEvents Instance
    {
        get
        {
            if (instance == null) Logging.LogRegularStringMessage(
                $"it is vital that {nameof(GameEvents)} is attached to a GameObject", ErrorSeverity.ScenePivotal);
            return instance;
        }
    }

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        CheckSingleton();
        // bad usage of events replace with direct method calls!
        SubscribeStaticEvents();
        PublishAllStaticEvents();
    }

    #endregion

    #region Custom Methods

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
    
    public void PublishOnRopeCut(RopeConnectors ropeConnector)
    {
        onRopeCut?.Invoke(ropeConnector);
    }

    #endregion

}