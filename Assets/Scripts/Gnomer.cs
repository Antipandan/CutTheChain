using System;
using UnityEngine;
using Utility;

public sealed class Gnomer : MonoBehaviour
{
    [SerializeField] private Animator gnomerAnimator;
    [SerializeField] private FindCandy findCandy;
    private bool isEatingCandy = false;
    private bool isCandyClose = false;
    private bool isGreating = false;

    public bool IsEatingCandy
    {
        get => isEatingCandy;
    }

    public bool IsCandyClose
    {
        get => isCandyClose;
    }

    public bool IsGreating
    {
        get => isGreating;
    }

    private void Awake()
    {
        CheckReferences();        
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref gnomerAnimator, gameObject, nameof(gnomerAnimator), ErrorSeverity.Warning);
        ReferenceValidator.CheckComponentForNull(ref findCandy, gameObject, nameof(findCandy), ErrorSeverity.Warning);
    }

    private void Update()
    {
        
    }

    private void UpdateAnimatorStates()
    {
        
    }
    
    
}
