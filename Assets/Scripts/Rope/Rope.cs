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
    [SerializeField] [CanBeNull] private Rigidbody2D connectedItem;
    [SerializeField] private RopeConnectors connectorPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    private List<Rigidbody2D> ropeComponents = new List<Rigidbody2D>();
    private float length = 1f;
    private Vector2 ropeVector = Vector2.down;

    private void Awake()
    {
        CheckReferences();
        if (connectedItem != null && anchor != null)
        {
            length = (connectedItem.transform.position - anchor.transform.position).magnitude;
        }
    }

    private void Start()
    {
        CalculateRopeVector();
        ropeComponents = new List<Rigidbody2D>() { anchor, connectedItem };
        SpawnJoints();
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
        for (int i = 0; i < resolution; i++)
        {
            float fraction = (1f + i) / (float)(resolution + 1f);
            Vector3 fractionPosition = ropeVector * length * fraction;
            RopeConnectors connector = Instantiate(connectorPrefab, fractionPosition, quaternion.identity)
                .GetComponent<RopeConnectors>();
            lineRenderer.positionCount++;
            AddRopeComponentProperly(connector.GetComponent<Rigidbody2D>());
            ConfigureJoint(connector);
        }
    }

    private void AddRopeComponentProperly(Rigidbody2D connector)
    {
        // try some new stuff
        List<Rigidbody2D> oldComponents = new List<Rigidbody2D>(ropeComponents.Count + 1) { ropeComponents[0] };
        for (int i = 1; i < ropeComponents.Capacity - 2; i++)
        {
            oldComponents.Add(ropeComponents[i]);
        }
        ropeComponents.Add(connector);
        oldComponents.Add(ropeComponents[^1]);
        ropeComponents = oldComponents;
    }

    [CanBeNull]
    private Rigidbody2D GetParentPoint(RopeConnectors child)
    {
        int index = ropeComponents.IndexOf(child.GetComponent<Rigidbody2D>()) - 1;
        if (index <= 0 || index >= ropeComponents.Count - 1) return null;
        return ropeComponents[index];
    }

    private void ConfigureJoint(RopeConnectors connector)
    {
        Rigidbody2D parentObject = GetParentPoint(connector);
        ConfigureJointLenght(connector.Joint, parentObject);
        ConfigureJointParent(connector.Joint, parentObject);
    }

    private void ConfigureJointLenght(DistanceJoint2D joint, Rigidbody2D parent)
    {
        float distance = (joint.gameObject.transform.position - parent.transform.position).magnitude;
        joint.distance = distance;
        joint.autoConfigureConnectedAnchor = false;
    }

    private void ConfigureJointParent(DistanceJoint2D joint, Rigidbody2D parent)
    {
        joint.connectedBody = parent;
    }
    
    // Är inte direkt en insert men anser att det är viktigare att få saker att fungera först
    //TODO fixa faktiskt insert istället för att ersätta
    private void Insertbodies(params Rigidbody2D[] newBodies)
    {
        
    }

    private List<Rigidbody2D> GetOldRelevantBodies()
    {
        return null;
    }

    private void InsertBody(Rigidbody2D body)
    {
        
    }

    private void UpdateLineRendererOrder()
    {
        
    }
    private void ConfigureRopeConnectors()
    {
        
    }

    private void CalculateJointPlacement()
    {
        
    }

    private void CalculateExtraJointPlacements()
    {
        
    }
}