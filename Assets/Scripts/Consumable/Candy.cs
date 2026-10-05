using UnityEngine;
using System;
using System.Collections.Generic;
using CustomUtility;

public class Candy : Attachable
{
    [SerializeField] private AnchorPosition anchorPoint;
    [SerializeField] private RandomSounds sounds;
    private static float fallSpeedMaxSound = GameConstants.maxFallSpeedSound;
    private static float minVolumeMultiplier = GameConstants.minVolumeImpactSoundMultiplier;
    private static float maxVolumeMultiplier = GameConstants.maxVolumeImpactSoundMultiplier;
    private static Candy instance;
    
    public static Candy Instance
    {
        get => instance;
    }
    
    private void Awake()
    {
        CheckReferences();
        CheckSingleton();
    }

    private void CheckSingleton()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    protected override void CheckReferences()
    {
        List<AnchoredJoint2D> existingJoints = new List<AnchoredJoint2D>(gameObject.GetComponents<AnchoredJoint2D>());
        for (int i = 0; i < existingJoints.Count; i++)
        {
            AnchoredJoint2D currentJoint = existingJoints[i];
        }
        base.CheckReferences();
    }
    
    public override void PositionAccordingToAttachPoint()
    {
        gameObject.transform.position = GetAttachPointPosition() - GetLocalAttachPosition();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (sounds == null) return;
        SoundPlayer player = SoundPlayerManager.RequestSoundPlayer(sounds.GetRandomSound(), CalculateThudVolume(other));
        player!.PlaySound();
    }

    private float CalculateThudVolume(Collision2D collisionObject)
    {
        float currentFallSpeed = collisionObject.relativeVelocity.magnitude;
        float volumeMultiplier = currentFallSpeed / fallSpeedMaxSound;
        float multiplier = Mathf.Clamp(volumeMultiplier, minVolumeMultiplier, maxVolumeMultiplier);
        return multiplier;
    }
}