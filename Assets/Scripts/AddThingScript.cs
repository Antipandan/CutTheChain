using System;
using UnityEngine;
using UnityEngine.Events;
using CustomUtility;

[RequireComponent(typeof(Collider2D))]
public abstract class AddThingScript<TValidTarget> : MonoBehaviour where TValidTarget : MonoBehaviour
{
    [Tooltip("Used to check if gameObject has a collider or not. Fill reference if possible")]
    [SerializeField] protected new Collider2D collider;
    [Tooltip("Optional event if user wants extra functionality")]
    [SerializeField] public UnityEvent itemFound;
    [Tooltip("Optional event if user wants extra functionality")]
    [SerializeField] public UnityEvent itemLost;

    #region Unity Lifecycle

    protected void Awake()
    {
        CheckReferences();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TValidTarget foundObject = other.gameObject.GetComponent<TValidTarget>();
        if (foundObject == null) return;
        itemFound?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TValidTarget foundObject = other.gameObject.GetComponent<TValidTarget>();
        if (foundObject == null) return;
        itemLost?.Invoke();
    }

    #endregion

    #region Custom Methods
    
    protected virtual void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref collider, gameObject, ErrorSeverity.Warning);
    }
    
    #endregion

}
