using UnityEngine;
using System;
using JetBrains.Annotations;
using Utility;
[RequireComponent(typeof(Rigidbody2D), typeof(DistanceJoint2D))]
public class RopeConnectors : MonoBehaviour
{
    [Tooltip("Reference should be filled. This component should exist on gameObject!")]
    [SerializeField] private Rigidbody2D jointRigidBody;
    [Tooltip("Reference should be filled. This component should exist on gameObject!")] 
    [SerializeField] private Collider2D jointCollider;
    [Tooltip("Reference should be filled. This component should exist on gameObject!")] 
    [SerializeField] private DistanceJoint2D joint;

    [NotNull]
    public DistanceJoint2D Joint
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

    private void Awake()
    {
        CheckReferences(out bool allgood);
    }

    private void CheckReferences(out bool allGood)
    {
        ReferenceValidator.CheckComponentForNull(ref jointRigidBody, gameObject, out bool value, nameof(jointRigidBody), ErrorSeverity.FatalError);
        ReferenceValidator.CheckComponentForNull(ref jointCollider, gameObject, out bool value1, nameof(jointCollider), ErrorSeverity.FatalError);
        ReferenceValidator.CheckComponentForNull(ref joint, gameObject, out bool value2, nameof(joint), ErrorSeverity.FatalError);
        allGood = value1 && value && value2;
    }

    public void ConfigureJoint(float distance)
    {
        if (joint == null) return;
        joint.autoConfigureDistance = false;
        joint.enableCollision = true;
        joint.distance = distance;
    }

    /// <summary>
    /// Change the gameObject to either be active or inactive. Set to inactive to make this rigidbody act as the endpoint if there is no candy attached
    /// </summary>
    public void ChangeJointStatus(bool status = true)
    {
        Debug.Log($"active!");
        joint.enabled = status;
    }

}