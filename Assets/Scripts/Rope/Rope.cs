using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Utility;

public class Rope : MonoBehaviour
{
    [SerializeField] [Range(0, 10000)] private uint Resolution;
    [SerializeField] [CanBeNull] private Rigidbody2D anchor;
    [SerializeField] [CanBeNull] private Rigidbody2D connectedItem;
    [SerializeField] private RopeConnectors connectorPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    private List<RopeConnectors> joints = new List<RopeConnectors>();
    private float length = 1f;

    private void Awake()
    {
        CheckReferences();
        if (connectedItem != null)
        {
            length = (connectedItem.transform.position - gameObject.transform.position).magnitude;
        }
    }

    private void Start()
    {
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

    private void SpawnJoints()
    {
        if (Resolution < 1) return; 
        for (int i = 0; i < Resolution; i++)
        {
            float fractionalPlacement = (i + 1) / (float)Resolution + 1f;
            RopeConnectors joint = Instantiate(connectorPrefab, length * (Vector3.down * fractionalPlacement),
                Quaternion.identity).GetComponent<RopeConnectors>();
            if (joint != null) joints.Add(joint);
        }
    }

    private void CalculateJointPlacement()
    {
        if (lineRenderer == null) return;
        for (int i = 0; i < joints.Count; i++)
        {
            lineRenderer.SetPosition(i, joints[i].transform.position);
        }
    }
}