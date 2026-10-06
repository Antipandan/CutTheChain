using System;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomUtility;
using Object = UnityEngine.Object;

[RequireComponent(typeof(CircleCollider2D))]
public class MouseCutter : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private static Camera mainCamera;

    #region Properties
    
    public static Camera MainCamera
    {
        get => mainCamera;
    }
    
    #endregion

    #region Unity Lifecycle
    
    private void Awake()
    {
        CheckReferences();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Mouse.current.leftButton.isPressed && other.gameObject.TryGetComponent(out RopeConnectors ropePart))
        {
            CutChain(ropePart);
        }
    }
    
    private void Update()
    {
        gameObject.transform.position = MousePositionWorldSpace();
    }
    
    #endregion

    #region Custom Methods

    
    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref circleCollider2D, gameObject, out bool wasNull, nameof(circleCollider2D));
        if (!wasNull) ConfigureCircleCollider2D(circleCollider2D);
        if (mainCamera == null) mainCamera = Camera.main;
    }
    

    private static void CutChain(RopeConnectors ropePart)
    {
        if (ropePart == null) return;
        Debug.Log($"rope!");
        ropePart.OnRopeCut();
    }

    private static void EnsureProperDestruction(RopeConnectors ropePart)
    {
        Rigidbody2D parentRigidbody = ropePart.JointRigidBody;
        if (parentRigidbody.gameObject.TryGetComponent(out HingeJoint2D hinge))
        {
            hinge.connectedBody = null;
            Destroy(ropePart.gameObject);
        }
    }
    public static void ConstructMouseCutter()
    {
        Object.Instantiate(new GameObject("Mouse Cutter"), MousePositionWorldSpace(), Quaternion.identity).AddComponent<MouseCutter>();            
    }

    private static void ConfigureCircleCollider2D(GameObject mouseObject)
    {
        CircleCollider2D col2D = mouseObject.GetComponent<CircleCollider2D>();
        col2D.radius = 0.2f;
        col2D.isTrigger = true;
    }

    private static void ConfigureCircleCollider2D(CircleCollider2D col2D)
    {
        col2D.radius = 0.2f;
        col2D.isTrigger = true;
    }

    private static Vector3 MousePositionWorldSpace()
    {
        return mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    #endregion

}