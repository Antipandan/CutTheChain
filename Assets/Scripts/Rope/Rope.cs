using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Utility;

public sealed class Rope : MonoBehaviour
{
    [SerializeField] [Range(0, 10000)] private uint resolution = 0;
    [SerializeField] [CanBeNull] private Rigidbody2D anchor;
    [SerializeField] [CanBeNull] private RopeConnectors connectedItem;
    [SerializeField] private RopeConnectors connectorPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    private List<RopeConnectors> ropeSegments = new List<RopeConnectors>();
    private Vector2 ropeVector = Vector2.down;
    private float ropeLength = 1f;

    private void Awake()
    {
        CheckReferences();
        if (connectedItem != null && anchor != null)
        {
            ropeLength = (connectedItem.transform.position - anchor.transform.position).magnitude;
        }
    }

    private void Start()
    {
        CalculateRopeVector();
        SpawnJoints();
        ConnectAllJoints();
    }
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref connectorPrefab, gameObject, nameof(connectorPrefab), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref lineRenderer, gameObject, nameof(lineRenderer), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref connectedItem, gameObject, nameof(connectedItem));
    }

    private void Update()
    {
        return;
    }

    private void CalculateRopeVector()
    {
        if (anchor == null || connectedItem == null) return;
        ropeVector = (connectedItem.gameObject.transform.position - anchor.gameObject.transform.position);
    }

    private void SpawnJoints()
    {
        for (int i = 0; i < resolution; i++)
        {
            float fraction = (1f + i) / (float)(resolution + 1f);
            Vector3 fractional = (ropeVector * fraction);
            if (anchor != null)
            {
                Vector3 fractionPosition = anchor.gameObject.transform.position + fractional;
                RopeConnectors connector = Instantiate(connectorPrefab, fractionPosition, quaternion.identity)
                    .GetComponent<RopeConnectors>();
                lineRenderer.positionCount++;
                ropeSegments.Add(connector);
            }
        }
    }

    private void ConnectAllJoints()
    {
        for (int i = 0; i < ropeSegments.Count; i++)
        {
            ConfigureJoint(ropeSegments[i]);
        }
        if (connectedItem == null) return;
        if (ropeSegments.Count < 0)
        {
            connectedItem.Joint.connectedBody = ropeSegments[^1].JointRigidBody;
            connectedItem.Joint.distance = (1 / 2f) * ropeLength;
        }
        else
        {
            connectedItem.Joint.connectedBody = anchor;
            connectedItem.Joint.distance = ropeLength;
        }
        connectedItem.ChangeJointStatus(true);

    }
    
    [CanBeNull]
    private Rigidbody2D GetParentPoint(RopeConnectors child)
    {
        int index = ropeSegments.IndexOf(child) - 1;
        if (index == -1) return anchor;
        if (index >= ropeSegments.Count) return connectedItem!.JointRigidBody;
        Debug.Log($"index: {index}");
        return ropeSegments[index].JointRigidBody;
    }

    private void ConfigureJoint(RopeConnectors connector)
    {
        Rigidbody2D parentObject = GetParentPoint(connector);
        if (parentObject == null) return;
        ConfigureJointLenght(connector.Joint, parentObject);
        ConfigureJointParent(connector.Joint, parentObject);
        connector.ChangeJointStatus(true);
    }
    
    private void ConfigureJointLenght(DistanceJoint2D joint, Rigidbody2D parent)
    {
        float distance = (1 / ((float)resolution + 1f)) * ropeLength;
        
        joint.distance = distance;
        joint.autoConfigureConnectedAnchor = false;
    }

    private void ConfigureJointParent(DistanceJoint2D joint, Rigidbody2D parent)
    {
        joint.connectedBody = parent;
    }
}