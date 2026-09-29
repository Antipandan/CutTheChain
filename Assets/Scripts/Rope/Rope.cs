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
    [SerializeField]  private RopeConnectors connectorPrefab;
    [SerializeField] [CanBeNull] private Rigidbody2D connectedItem;

    private void Awake()
    {
        CheckReferences();
    }

    private void SpawnConnectors()
    {
        
    }

    private float CalculateFractionalValue(int nominatorOffset)
    {
        return (1f + nominatorOffset) / (float)(resolution + 1f);
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref connectorPrefab, gameObject, nameof(connectorPrefab) ,ErrorSeverity.FatalError);
        ReferenceValidator.CheckComponentForNull(ref connectedItem, gameObject, nameof(connectedItem) ,ErrorSeverity.Warning);
        if (resolution >= 100) Logging.LogPrimitiveHighValueError(nameof(resolution), ErrorSeverity.Warning, gameObject);
    }
}