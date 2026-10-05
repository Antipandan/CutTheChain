using System;
using UnityEngine;
using CustomUtility;

public class AnchorPosition : MonoBehaviour
{
    [Tooltip("Gameobject should have a component that is the desired type. This type will be sent to the parent.")]
    [SerializeField] private AnchoredJoint2D anchorJoint;
    private GameObject parentGameObject;
    
    #region Unity Lifecycle
    
    private void Awake()
    {
        if (parentGameObject == null) parentGameObject = transform.parent.gameObject;
        EnsureIsChild();
        ConfigureJoint();
        parentGameObject.AddComponent<AnchoredJoint2D>();
    }
    
    #endregion

    #region Custom Methods

    private void EnsureIsChild()
    {
        if (parentGameObject.GetComponentInChildren<Transform>() == transform) return;
        gameObject.transform.SetParent(parentGameObject.transform);
    }

    private void ConfigureJoint()
    {
        // This assumes that said gameObject is a child of the parent
        anchorJoint.anchor = gameObject.transform.localPosition;
    }

    #endregion
    
}