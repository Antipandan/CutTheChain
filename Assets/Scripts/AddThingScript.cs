using System;
using UnityEngine;
using UnityEngine.Events;
using Utility;

[RequireComponent(typeof(Collider2D))]
public abstract class AddThingScript<TValidTarget> : MonoBehaviour where TValidTarget : MonoBehaviour
{
    [Tooltip("Used to check if gameObject has a collider or not. Fill reference if possible")]
    [SerializeField] private new Collider2D collider;
    [Tooltip("Optional event if user wants extra functionality")]
    [SerializeField] protected UnityEvent enemyFound;
    [Tooltip("Optional event if user wants extra functionality")]
    [SerializeField] protected UnityEvent enemyLost;
    public Action<TValidTarget> onItemFound;
    public Action<TValidTarget> OnItemDisappear;

    public int NrFoundEnemySubscribedEvents
    {
        get => onItemFound is null ? 0 : onItemFound.GetInvocationList().Length;
    }

    public int NrLostEnemySubscribedEvents
    {
        get => OnItemDisappear is null ? 0 : OnItemDisappear.GetInvocationList().Length;
    }

    protected void Awake()
    {
        ReferenceValidator.CheckComponentForNull(ref collider, gameObject, ErrorSeverity.Warning);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TValidTarget foundObject = other.gameObject.GetComponent<TValidTarget>();
        if (foundObject is null) return;
        onItemFound?.Invoke(foundObject);
        enemyFound?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TValidTarget foundObject = other.gameObject.GetComponent<TValidTarget>();
        if (foundObject is null) return;
        OnItemDisappear?.Invoke(foundObject);
        enemyLost?.Invoke();
    }
}
