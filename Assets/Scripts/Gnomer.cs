using System;
using UnityEngine;
using Utility;

public sealed class Gnomer : MonoBehaviour
{
    [SerializeField] private Animator gnomerAnimator;
    [SerializeField] private FindCandy findCandy;

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
