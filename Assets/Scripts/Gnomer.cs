using System;
using System.Collections;
using UnityEngine;
using Utility;

public sealed class Gnomer : MonoBehaviour
{
    [SerializeField] private GnomerAnimationHandler gnomerAnimationHandler;
    [SerializeField] private FindCandy findCandy;
    [SerializeField] [Range(0f, 360f)] private float greetDelay = 15f;
    
    
    #region Unity Lifecycle Methods

    private void Awake()
    {
        CheckReferences();
        StartCoroutine(Greet());
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Candy candy))
        {
            // OnBeginConsumeCandy(candy);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Candy candy))
        {
            OnCandyDetected();
        }
    }

    #endregion

    #region CustomMethods
    
    private void SubscribeEvents()
    {
        if (findCandy != null)
        {
            findCandy.itemFound.RemoveListener(OnCandyDetected);
            findCandy.itemFound.AddListener(OnCandyDetected); 
            findCandy.itemLost.RemoveListener(OnCandyLost);
            findCandy.itemLost.AddListener(OnCandyLost);
            findCandy.itemLost.RemoveListener(OnCloseMouthStart);
            findCandy.itemLost.AddListener(OnCloseMouthStart);
        }
    }

    // add in animator
    private void OnOpenMouthEnd()
    {
        gnomerAnimationHandler.GnomerAnimator.speed = 0f;
    }

    // preferable internal use
    private void OnCloseMouthStart()
    {
        return;
        gnomerAnimationHandler.GnomerAnimator.speed = 1f;
    }

    private void UnSubscribeEvents()
    {
        if (findCandy != null)
        {
            findCandy.itemFound.RemoveListener(OnCandyDetected);
            findCandy.itemLost.RemoveListener(OnCandyLost);
            findCandy.itemLost.RemoveListener(OnCloseMouthStart);
        }
    }

    private void OnCandyDetected()
    {
        gnomerAnimationHandler.GnomerAnimator.speed = 1f;
        gnomerAnimationHandler.IsCandyClose = true;
    }

    private void OnCandyLost()
    {
        gnomerAnimationHandler.IsCandyClose = false;
        gnomerAnimationHandler.GnomerAnimator.speed = 1f;
    }
    
    private void OnBeginConsumeCandy(Candy candy)
    {
        gnomerAnimationHandler.IsEatingCandy = true;
        gnomerAnimationHandler.IsCandyClose = false;
        Destroy(candy.gameObject);
    }
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref gnomerAnimationHandler, gameObject, nameof(gnomerAnimationHandler), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref findCandy, gameObject, nameof(findCandy), ErrorSeverity.Warning);
    }

    // stupid animations
    private void StopGreeting()
    {
        gnomerAnimationHandler.IsGreeting = false;
    }

    private void SetAnimationSpeed(float newSpeed = 1f)
    {
        gnomerAnimationHandler.GnomerAnimator.speed = newSpeed;
    }

    private IEnumerator Greet()
    {
        while (true)
        {
            yield return new WaitForSeconds(greetDelay);
            gnomerAnimationHandler.IsGreeting = true;
        }
        // ReSharper disable once IteratorNeverReturns
    }

    // intended to be used when using the animator
    private void PlaySomeSound(AudioClip clip)
    {
        SoundPlayerManager.RequestSoundPlayer(clip)?.PlaySound();
    }
    
    #endregion
    
}
