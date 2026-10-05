using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private float duration = 0f;
    private AudioSource audioSource;

    #region Properties

    public float Duration
    {
        get => duration;
        set => duration = Mathf.Max(value, 0f);
    }

    public AudioClip AudioClip
    {
        get => audioSource.clip;
    }

    #endregion
    
    #region Unity Lifecycle
    
    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }
    
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    
    #endregion
    
    #region Custom Methods
    
    private void SetupPlayer(AudioSource source, float duration)
    {
        this.duration = duration;
        this.audioSource = source;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PlaySound()
    {
        audioSource.Play();
        StartCoroutine(Destroy());
    }
    
    #endregion
    
}