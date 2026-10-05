using UnityEngine;
using System;
using Random = System.Random;

[CreateAssetMenu(fileName = "RandomSounds", menuName = "ScriptableObjects/RandomSounds")]
public sealed class RandomSounds : ScriptableObject
{
    [SerializeField] [Range(0, 1f)] private float volumeScaler = 1.0f;
    [SerializeField] private AudioClip[] sounds;
    private static Random random = new Random();

    #region Properties

    public float VolumeScaler
    {
        get => volumeScaler;
    }
    
    public static Random @Random
    {
        get => random;
    }

    #endregion

    #region Custom Methods

    public AudioClip GetRandomSound()
    {
        if (sounds.Length == 0) return null;
        if (sounds.Length == 1) return sounds[0];
        return sounds[random.Next(sounds.Length)];
    }

    public AudioClip GetRandomSound(Random rand)
    {
        if (sounds.Length == 0) return null;
        if (sounds.Length == 1) return sounds[0];
        return sounds[rand.Next(sounds.Length)];
    }

    #endregion

}