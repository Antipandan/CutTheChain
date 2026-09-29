using UnityEngine;
using System;
using Utility;

public class Candy : Attachable
{
    private static Candy instance;
    private void Awake()
    {
        CheckReferences();
        if (instance == null) instance = this;
        else Destroy(this);
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