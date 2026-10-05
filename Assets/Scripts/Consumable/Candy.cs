using UnityEngine;
using System;
using System.Collections.Generic;
using CustomUtility;
using JetBrains.Annotations;

public class Candy : Attachable
{
    [SerializeField] private AnchorPosition anchorPoint;
    [SerializeField] [CanBeNull] private RandomSounds sounds;
    private const float fallSpeedMaxSound = GameConstants.maxFallSpeedSound;
    private const float minVolumeMultiplier = GameConstants.minVolumeImpactSoundMultiplier;
    private const float maxVolumeMultiplier = GameConstants.maxVolumeImpactSoundMultiplier;
    private static Candy instance;

    #region MyRegion

    public static Candy Instance
    {
        get => instance;
    }

    public static float FallSpeedMaxSound
    {
        get => fallSpeedMaxSound;
    }

    public static float MinVolumeMultiplier
    {
        get => minVolumeMultiplier;
    }

    public static float MaxVolumeMultiplier
    {
        get => maxVolumeMultiplier;
    }

    public AnchorPosition AnchorPoint
    {
        get => anchorPoint;
    }

    public RandomSounds Sounds
    {
        get => sounds;
    }

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        CheckReferences();
        CheckSingleton();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (sounds == null) return;
        SoundPlayer player = SoundPlayerManager.RequestSoundPlayer(sounds.GetRandomSound(), CalculateThudVolume(other));
        player!.PlaySound();
    }

    #endregion

    #region Custom Methods

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
            ReferenceValidator.CheckComponentForNull(ref currentJoint, gameObject, nameof(currentJoint), ErrorSeverity.Warning);
        }
        base.CheckReferences();
    }
    
    public override void PositionAccordingToAttachPoint()
    {
        gameObject.transform.position = GetAttachPointPosition() - GetLocalAttachPosition();
    }

    private static float CalculateThudVolume(Collision2D collisionObject)
    {
        float currentFallSpeed = collisionObject.relativeVelocity.magnitude;
        float volumeMultiplier = currentFallSpeed / fallSpeedMaxSound;
        float multiplier = Mathf.Clamp(volumeMultiplier, minVolumeMultiplier, maxVolumeMultiplier);
        return multiplier;
    }

    #endregion
    
}