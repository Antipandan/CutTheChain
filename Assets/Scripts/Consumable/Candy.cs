using UnityEngine;
using System;
using System.Collections.Generic;
using Utility;

public class Candy : Attachable
{
    [SerializeField] private AnchorPosition anchorPoint;
    private static Candy instance;
    
    private void Awake()
    {
        CheckReferences();
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void SetupJoints()
    {
        
    }

    protected override void CheckReferences()
    {
        List<AnchoredJoint2D> existingJoints = new List<AnchoredJoint2D>(gameObject.GetComponents<AnchoredJoint2D>());
        for (int i = 0; i < existingJoints.Count; i++)
        {
            AnchoredJoint2D currentJoint = existingJoints[i];
        }
        base.CheckReferences();
    }

    private void PositionAnchorPoint()
    {
        
    }

    public static Candy Instance
    {
        get => instance;
    }
    
    public override void PositionAccordingToAttachPoint()
    {
        gameObject.transform.position = GetAttachPointPosition() - GetLocalAttachPosition();
    }


    
}