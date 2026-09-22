using UnityEngine;
using System;
using System.Collections.Generic;
using Utility;

[RequireComponent(typeof(LineRenderer))]
public sealed class Spring : MonoBehaviour
{
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private Rigidbody2D targetRigidbody;
    [SerializeField] private float springConstant = 1f;
    [SerializeField] [Range(0f, 100f)] private float springMaxLength = GameConstants.maxLength;
    [SerializeField] [Range(0f, 100f)] private float forceMultiplier = 1f;
    [SerializeField] private float springLength = 3/2f;
    private GameObject targetGameObject;
    private LineRenderer lineRenderer;
    private void Awake()
    {
        ReferenceValidator.CheckComponentForNull(ref lineRenderer, gameObject, nameof(lineRenderer));
        ReferenceValidator.CheckComponentForNull(ref anchorPoint, gameObject, nameof(anchorPoint));
        ReferenceValidator.CheckComponentForNull(ref targetRigidbody, gameObject, nameof(targetRigidbody));
        if (targetRigidbody != null) targetGameObject = targetRigidbody.gameObject;
    }

    private void Start()
    {
        SetupData();
    }
    
    private void OnValidate()
    {
        ValidateData();
    }
    
    public void Update()
    {
        if (targetGameObject == null || anchorPoint == null) return;
        DrawLine();
        ApplyHooksLaw();
    }

    private void SetupData()
    {
        if (lineRenderer != null) lineRenderer.positionCount = 2;
    }

    private void ValidateData()
    {
        if (springLength < 0) Logging.LogPrimitiveLowValueError(nameof(springLength), ErrorSeverity.Error, gameObject);
    }

    private void ApplyHooksLaw()
    {
        if (targetRigidbody == null)
        {
            targetRigidbody.AddForce(CalculateForceDirectionNormalized() * (forceMultiplier * Time.deltaTime), ForceMode2D.Force);
        }
    }

    private void DrawLine()
    {
        if (lineRenderer == null) return;
        lineRenderer.SetPosition(0, anchorPoint.position);
        lineRenderer.SetPosition(1, targetGameObject.transform.position);
    }

    private Vector2 CalculateForceDirectionNormalized()
    {
        Vector2 deltaPosition = targetRigidbody.transform.position - anchorPoint.position;
        float length = deltaPosition.magnitude;
        return Vector2.one * (-springConstant * length - springLength);
    }
    
}
