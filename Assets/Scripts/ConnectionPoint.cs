using UnityEngine;
using System;
using JetBrains.Annotations;
using Utility;

[RequireComponent(typeof(Rigidbody2D))]
public class ConnectionPoint : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    private bool connected = false;
    private void Awake()
    {
        
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref rigidBody, gameObject, nameof(rigidBody));
    }

    private void ConfigureRigidbody()
    {
        
    }

    public void AttemptConnect(RopeConnectors connector)
    {
        if (!connected) connector.Joint.connectedBody = rigidBody;
    }
}