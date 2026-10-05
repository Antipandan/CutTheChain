using System;
using System.Collections;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public static class SoundPlayerManager
{
    #region Custom Methods

    [CanBeNull]
    public static SoundPlayer RequestSoundPlayer([CanBeNull] AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (clip == null) return null;
        SoundPlayer player = ConstructSoundPlayer(out AudioSource source);
        ConfigureAudioSource(source, clip, volume, pitch);
        ConfigureSoundPlayer(player);
        return player;
    }

    public static SoundPlayer RequestSoundPlayerAtPosition([CanBeNull] AudioClip clip, float volume = 0f,
        Vector3 position = default)
    {
        SoundPlayer player = RequestSoundPlayer(clip, volume);
        if (player == null) return null;
        player.gameObject.transform.position = position;
        return player;
    }

    private static void ConfigureSoundPlayer(SoundPlayer soundPlayer)
    {
        // work around because this value cannot be initalized in awake because
        // we assign AudioClip after awake. This is because this class is structured abhorrently
        soundPlayer.Duration = soundPlayer.AudioClip.length;
    }

    private static void ConfigureSoundPlayer(SoundPlayer soundPlayer, float customDuration)
    {
        soundPlayer.Duration = customDuration;
    }

    private static void ConfigureAudioSource(AudioSource audioSource, AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        audioSource.clip = clip;
        audioSource.volume = EnsureVolumeCorrect(volume);
        audioSource.pitch = EnsurePitchCorrect(pitch);
    }

    private static SoundPlayer ConstructSoundPlayer(out AudioSource source)
    {
        GameObject soundPlayerObj = new GameObject("SoundPlayer");
        source = soundPlayerObj.AddComponent<AudioSource>();
        SoundPlayer player = soundPlayerObj.AddComponent<SoundPlayer>();
        return player;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float EnsureVolumeCorrect(float volume)
    {
        return Mathf.Max(volume, 0f);
    }
    
    private static float EnsurePitchCorrect(float pitch)
    {
        return Mathf.Clamp(pitch, -3f, 3f);
    }

    #endregion
}