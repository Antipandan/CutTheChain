using UnityEngine;
using System;
using JetBrains.Annotations;
using CustomUtility;
[RequireComponent(typeof(Rigidbody2D), typeof(HingeJoint2D))]
public class RopeConnectors : MonoBehaviour
{
    [Tooltip("Reference should be filled. This component should exist on gameObject!")]
    [SerializeField] private Rigidbody2D jointRigidBody;
    [Tooltip("Reference should be filled. This component should exist on gameObject!")] 
    [SerializeField] private Collider2D jointCollider;
    [Tooltip("Reference should be filled. This component should exist on gameObject!")] 
    [SerializeField] private HingeJoint2D joint;
    [Tooltip("Reference should be filled. This component should exist on gameObject!")] 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Tooltip("Audio to be played when cut")]
    [SerializeField] private AudioClip cutSound;

    #region Properties

    [NotNull]
    public HingeJoint2D Joint
    {
        get => joint;
    }

    [NotNull]
    public Rigidbody2D JointRigidBody
    {
        get => jointRigidBody;
    }

    [CanBeNull]
    public Collider2D JointCollider2D
    {
        get => jointCollider;
    }

    public Bounds SpriteBounds
    {
        get => spriteRenderer.bounds;
    }

    public float SpriteLength
    {
        get => spriteRenderer.sprite.bounds.size.x;
    }

    public float SpriteWidth
    {
        get => spriteRenderer.sprite.bounds.size.y;
    }

    [CanBeNull]
    public AudioClip CutSound
    {
        get => cutSound;
    }

    #endregion

    #region Unity Lifecycle
    
    private void Awake()
    {
        CheckReferences(out bool allgood);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    
    #endregion

    #region Custom Methods

    private static void SubscribeEvents()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.onRopeCut -= OnRopeCut;
            GameEvents.Instance.onRopeCut += OnRopeCut;
        }
    }

    private static void UnSubscribeEvents()
    {
        if (GameEvents.Instance != null)
        {
            GameEvents.Instance.onRopeCut -= OnRopeCut;
        }
    }

    private void CheckReferences(out bool allGood)
    {
        ReferenceValidator.CheckComponentForNull(ref jointRigidBody, gameObject, out bool value, nameof(jointRigidBody), ErrorSeverity.FatalError);
        ReferenceValidator.CheckComponentForNull(ref jointCollider, gameObject, out bool value1, nameof(jointCollider), ErrorSeverity.FatalError);
        ReferenceValidator.CheckComponentForNull(ref joint, gameObject, out bool value2, nameof(joint), ErrorSeverity.FatalError);
        allGood = value1 && value && value2;
    }

    /// <summary>
    /// Change the gameObject to either be active or inactive. Set to inactive to make this rigidbody
    /// act as the endpoint if there is no candy attached
    /// </summary>
    public void ChangeJointStatus(bool status = true)
    {
        joint.enabled = status;
    }

    private static void OnRopeCut(RopeConnectors ropeConnectors)
    {
        if (ropeConnectors.cutSound != null) SoundPlayerManager.RequestSoundPlayer(ropeConnectors.cutSound)?.PlaySound();
        ropeConnectors.gameObject.SetActive(false);
        Destroy(ropeConnectors.gameObject);
    }

    #endregion
    
}