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
    private List<Rigidbody2D> segments = new List<Rigidbody2D>();
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
        InitializeFillSegments();
        CalculateRopeVector();
        SpawnJoints();
        ConnectAllJoints();
    }

    
    private void InitializeFillSegments()
    {
        segments = new List<Rigidbody2D>(){anchor};
        if (connectedItem != null) segments.Add(connectedItem.JointRigidBody);
    }
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref connectorPrefab, gameObject, nameof(connectorPrefab), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref lineRenderer, gameObject, nameof(lineRenderer), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref connectedItem, gameObject, nameof(connectedItem));
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            lineRenderer.SetPosition(i, segments[i].transform.position);
        }
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
            float fraction = CalculateFractionalLength(i);
            Vector3 fractional = (ropeVector * fraction);
            if (anchor != null)
            {
                Vector3 fractionPosition = anchor.gameObject.transform.position + fractional;
                RopeConnectors connector = Instantiate(connectorPrefab, fractionPosition, quaternion.identity)
                    .GetComponent<RopeConnectors>();
                lineRenderer.positionCount++;
                ropeSegments.Add(connector);
                AddSegmentProperly(connector.JointRigidBody);
            }
        }
    }

    private void AddSegmentProperly(Rigidbody2D ropeSegment)
    {
        List<Rigidbody2D> newRopeSegment = new List<Rigidbody2D>(segments.Count + 1);
        newRopeSegment.Add(anchor);
        for (int i = 1; i < segments.Count - 1; i++)
        {
            newRopeSegment.Add(segments[i]);
        }
        newRopeSegment.Add(ropeSegment);
        if (connectedItem != null) newRopeSegment.Add(connectedItem.JointRigidBody);
        segments = newRopeSegment;
    }

    private void ConnectAllJoints()
    {
        for (int i = 0; i < ropeSegments.Count; i++)
        {
            ConfigureJoint(ropeSegments[i]);
        }
        if (connectedItem == null) return;
        ConnectLastItemProperly();
    }

    private void ConnectLastItemProperly()
    {
        if (connectedItem == null) return;
        
        if (ropeSegments.Count > 0)
        {
            connectedItem.Joint.connectedBody = ropeSegments[^1].JointRigidBody;
            connectedItem.Joint.distance = CalculateFractionalLength() * ropeLength;
        }
        else
        {
            connectedItem.Joint.connectedBody = anchor;
            connectedItem.Joint.distance = ropeLength;
        }
        
        connectedItem.ChangeJointStatus(true);
    }

    private float CalculateFractionalLength(int nominatorOffset = 0)
    {
        return (1 + nominatorOffset) / ((float)(resolution) + 1);
    }
    
    [CanBeNull]
    private Rigidbody2D GetParentPoint(RopeConnectors child)
    {
        int index = ropeSegments.IndexOf(child) - 1;
        if (index == -1) return anchor;
        if (index >= ropeSegments.Count) return connectedItem!.JointRigidBody;
        return ropeSegments[index].JointRigidBody;
    }

    private void ConfigureJoint(RopeConnectors connector)
    {
        // Debug.Log($"connector: {connector.gameObject.name}");
        Rigidbody2D parentObject = GetParentPoint(connector);
        // Debug.Log($"parent: {parentObject.gameObject.name}");
        if (parentObject == null) return;
        ConfigureJointLenght(connector.Joint, parentObject);
        ConfigureJointParent(connector.Joint, parentObject);
        connector.ChangeJointStatus(true);
    }
    
    private void ConfigureJointLenght(DistanceJoint2D joint, Rigidbody2D parent)
    {
        float distance = CalculateFractionalLength() * ropeLength;
        
        joint.distance = distance;
        joint.autoConfigureConnectedAnchor = false;
    }

    private void ConfigureJointParent(DistanceJoint2D joint, Rigidbody2D parent)
    {
        joint.connectedBody = parent;
    }
}