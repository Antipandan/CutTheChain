using UnityEngine;
using System;
using Utility;

[RequireComponent(typeof(Rigidbody2D), typeof(HingeJoint2D))]
public class Anchor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private HingeJoint2D joint;
    [SerializeField] private RopeConnectors connector;

    private void Awake()
    {
        ReferenceValidator.CheckComponentForNull(ref connector, gameObject, out bool connectorNull, nameof(connector));
        if (!connectorNull) InitializeConnector();
        ReferenceValidator.CheckComponentForNull(ref rigidBody, gameObject, out bool value, nameof(rigidBody));
        ReferenceValidator.CheckComponentForNull(ref joint, gameObject, out bool value2, nameof(joint));
    }
    
    private void InitializeConnector()
    {
        Rigidbody2D rigidBody2D = connector.JointRigidBody;
        rigidBody2D.gravityScale = 0f;
        rigidBody2D.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    
    
}