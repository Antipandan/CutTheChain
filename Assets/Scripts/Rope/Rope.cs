using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Utility;

public sealed class Rope : MonoBehaviour
{
    [SerializeField] [Range(0, 10000)] private uint Resolution = 0;
    [SerializeField] [CanBeNull] private Rigidbody2D anchor;
    [SerializeField] [CanBeNull] private Rigidbody2D connectedItem;
    [SerializeField] private RopeConnectors connectorPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    private List<RopeConnectors> joints = new List<RopeConnectors>();
    private List<Rigidbody2D> bodies = new List<Rigidbody2D>(2);
    private float length = 1f;
    private Vector2 ropeVector = Vector2.down;

    private void Awake()
    {
        CheckReferences();
        PrepopulateBodies();
        if (connectedItem != null && anchor != null)
        {
            length = (connectedItem.transform.position - anchor.transform.position).magnitude;
        }
    }

    private void Start()
    {
        CalculateRopeVector();
        SpawnJoints();
        ConfigureRopeConnectors();
    }
    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref connectorPrefab, gameObject, nameof(connectorPrefab), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref lineRenderer, gameObject, nameof(lineRenderer), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref connectedItem, gameObject, nameof(connectedItem));
    }
    
    private void OnValidate()
    {
    #if UNITY_EDITOR
        if (!(length < 1f) && !(length > 100f)) return;
        Logging.LogPrimitiveOddValueError(nameof(length), ErrorSeverity.Error);
    #endif
    }

    private void Update()
    {
        CalculateJointPlacement();
    }

    private void CalculateRopeVector()
    {
        if (anchor == null || connectedItem == null) return;
        ropeVector = -(anchor.gameObject.transform.position - connectedItem.gameObject.transform.position).normalized;
    }

    private void SpawnJoints()
    {
        if (Resolution < 1) return; 
        for (int i = 0; i < Resolution; i++)
        {
            float fractionalPlacement = (i + 1) / (Resolution + 1f);
            RopeConnectors joint = Instantiate(connectorPrefab, length * (ropeVector * fractionalPlacement),
                Quaternion.identity).GetComponent<RopeConnectors>();
            if (joint == null) continue;
            lineRenderer.positionCount++;
            joints.Add(joint);
            InsertBody(joint.GetComponent<Rigidbody2D>());
            ConfigureJoint(joint);
        }
    }

    [CanBeNull]
    private Rigidbody2D GetParentPoint(RopeConnectors child)
    {
        int index = bodies.IndexOf(child.GetComponent<Rigidbody2D>());
        if (index <= 0 || index >= bodies.Count - 1) return null;
        Debug.Log($"parent point: {bodies[index].name}");
        return bodies[index];
    }

    private void ConfigureJoint(RopeConnectors connector)
    {
        DistanceJoint2D joint = connector.Joint;
        if (joint == null) return;
        if (!joints.Contains(connector)) return;
        Rigidbody2D parent = GetParentPoint(connector);
        Debug.Log($"child: {gameObject.name}, parent: {parent.gameObject.name}");
        if (parent == null)
        {
            Debug.Log($"parent was null");
            return;
        }
        ConfigureJointLenght(connector, joint, parent);
        ConfigureJointParent(connector, joint, parent);

    }

    private void ConfigureJointLenght(RopeConnectors connector, DistanceJoint2D joint, Rigidbody2D parent = null)
    {
        Debug.Log($"configure length");
        Rigidbody2D lastItem = parent == null ? GetParentPoint(connector) : parent;
        if (lastItem == null) return;
        int currentIndex = joints.IndexOf(connector);
        Vector3 lastItemPosition = lastItem.transform.position; 
        Vector3 currentItemPosition = joints[currentIndex].transform.position;
        joint.distance = (lastItemPosition - currentItemPosition).magnitude;
        joint.autoConfigureDistance = false;
    }

    private void ConfigureJointParent(RopeConnectors connector, DistanceJoint2D joint, Rigidbody2D parent = null)
    {
        Debug.Log($"configure parent");
        Rigidbody2D lastItem = parent == null ? GetParentPoint(connector) : parent;
        if (lastItem == null) return;
        joint.connectedBody = lastItem.GetComponent<Rigidbody2D>();
        
    }

    private void PrepopulateBodies()
    {
        bodies.Add(anchor);
        bodies.Add(connectedItem);
    }
    
    // Är inte direkt en insert men anser att det är viktigare att få saker att fungera först
    //TODO fixa faktiskt insert istället för att ersätta
    private void Insertbodies(params Rigidbody2D[] newBodies)
    {
        List<Rigidbody2D> oldBodies = GetOldRelevantBodies();
        bodies = new List<Rigidbody2D>(bodies.Count + 2);
        bodies.Add(anchor);
        bodies.AddRange(oldBodies);
        bodies.AddRange(newBodies);
        bodies.Add(connectedItem);
    }

    private List<Rigidbody2D> GetOldRelevantBodies()
    {
        return bodies.GetRange(1, bodies.Count - 2);
    }

    private void InsertBody(Rigidbody2D body)
    {
        bodies = new List<Rigidbody2D>(bodies.Count);
        bodies.Add(anchor);
        bodies.Add(body);
        bodies.Add(connectedItem);
        // UpdateLineRendererOrder();
    }

    private void UpdateLineRendererOrder()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            Debug.Log($"joints: {bodies[i].name} at: {i}");
            lineRenderer.SetPosition(i, bodies[i].transform.position);
        }
    }
    private void ConfigureRopeConnectors()
    {
        for (int i = 0; i < joints.Count; i++)
        {
            // Rider
            if (anchor == null) continue;
            float distance = (anchor.gameObject.transform.position - joints[i].gameObject.transform.position).magnitude;
            joints[i].ConfigureJoint(distance);
        }
    }

    private void CalculateJointPlacement()
    {
        if (lineRenderer == null) return;
        if (anchor != null) lineRenderer.SetPosition(0, anchor.gameObject.transform.position);
        if (connectedItem != null) lineRenderer.SetPosition(lineRenderer.positionCount - 1, connectedItem.gameObject.transform.position);
        CalculateExtraJointPlacements();
    }

    private void CalculateExtraJointPlacements()
    {
        for (int i = 1; i <= joints.Count; i++)
        {
            lineRenderer.SetPosition(i, joints[i - 1].transform.position);
        }
    }
}