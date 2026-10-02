using System;
using UnityEngine;
using UnityEngine.Events;
using Utility;

public sealed class Star : MonoBehaviour
{
    [SerializeField] private RandomSounds randomSoundses;
    [SerializeField] public UnityEvent onStarCollectedEvent;
    public static Action onCollectedStar;
    private static event Action<Star> onStarCollected;
    
    public static int NrOnStarCollected
    {
        get => onStarCollected == null ? 0 : onStarCollected.GetInvocationList().Length;
    }

    public static int NrCollectedStar
    {
        get => onCollectedStar == null ? 0 : onCollectedStar.GetInvocationList().Length;
    }

    public static Delegate[] CollectedStarDelegates
    {
        get => onCollectedStar?.GetInvocationList();
    }

    private void Awake()
    {
        SubscribeEvents();   
        Setup();
    }

    private void OnEnable()
    {
        if (NrOnStarCollected == 0) SubscribeEvents();
    }

    private void OnDisable()
    {
        if (NrOnStarCollected != 0) UnsubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void Setup()
    {
        if (randomSoundses == null) Logging.LogNullReferenceError(nameof(randomSoundses), ErrorSeverity.Warning, gameObject);
    }

    private void CollectStar(Star star)
    {
        PublishOnStarCollectedEvent();
        AudioClip clip = randomSoundses == null ? null : randomSoundses.GetRandomSound();
        if (clip != null)
        {
            SoundPlayer player = SoundPlayerManager.RequestSoundPlayer(clip);
            // This will with a high likelihood not be null since clip is not null.
            player!.PlaySound();
        }
        Destroy(star.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Candy _)) PublishOnStarCollected(this);
    }

    private void SubscribeEvents()
    {
        onStarCollected += CollectStar;
    }

    private void UnsubscribeEvents()
    {
        onStarCollected -= CollectStar;
    }

    private void PublishOnStarCollectedEvent()
    {
        onStarCollectedEvent.Invoke();
    }

    private static void PublishOnStarCollected(Star star)
    {
        onCollectedStar?.Invoke();
        onStarCollected?.Invoke(star);
    }
}
