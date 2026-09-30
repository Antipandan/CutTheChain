using UnityEngine;
using System;
using Random = System.Random;

[CreateAssetMenu(fileName = "StarPickupSound", menuName = "ScriptableObjects/Star Pickup Sound")]
public sealed class StarPickupSound : ScriptableObject
{
    [SerializeField] private AudioClip[] starPickupSounds;
    private static Random random = new Random();

    public AudioClip GetRandomSound()
    {
        if (starPickupSounds.Length == 0) return null;
        if (starPickupSounds.Length == 1) return starPickupSounds[0];
        return starPickupSounds[random.Next(starPickupSounds.Length)];
    }

    public AudioClip GetRandomStarSound(Random rand)
    {
        if (starPickupSounds.Length == 0) return null;
        if (starPickupSounds.Length == 1) return starPickupSounds[0];
        return starPickupSounds[rand.Next(starPickupSounds.Length)];
    }
}