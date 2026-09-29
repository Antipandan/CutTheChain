using JetBrains.Annotations;
using UnityEngine;
using Utility;

[RequireComponent(typeof(Rigidbody2D), typeof(HingeJoint2D))]
public abstract class Attachable : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigidBody2D;
    [SerializeField] [CanBeNull] protected CircleCollider2D circleCollider;
    [SerializeField] protected HingeJoint2D joint;

    protected virtual void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref rigidBody2D, gameObject,nameof(rigidBody2D));
        ReferenceValidator.CheckComponentForNull(ref circleCollider, gameObject, nameof(circleCollider));
        ReferenceValidator.CheckComponentForNull(ref joint, gameObject, nameof(joint));
    }

    public virtual Transform GetAttachmentPoint()
    {
        return gameObject.transform;
    }

    protected virtual Vector3 GetLocalAttachPosition()
    {
        return gameObject.transform.localPosition;
    }

    protected virtual Vector3 GetAttachPointPosition()
    {
        return gameObject.transform.position; 
    }
    
    public virtual void PositionAccordingToAttachPoint()
    {
        return;
    }
}