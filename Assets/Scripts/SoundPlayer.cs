using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private float duration = 0f;
    private AudioSource audioSource;

    public float Duration
    {
        get => duration;
        set => duration = Mathf.Max(value, 0f);
    }

    public AudioClip AudioClip
    {
        get => audioSource.clip;
    }

    private void SetupPlayer(AudioSource source, float duration)
    {
        this.duration = duration;
        this.audioSource = source;
    }

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
        Destroy(this.gameObject);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PlaySound()
    {
        audioSource.Play();
        StartCoroutine(Destroy());
    }
    
}