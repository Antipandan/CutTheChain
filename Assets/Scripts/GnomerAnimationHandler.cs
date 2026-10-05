using System;
using UnityEngine;
using CustomUtility;

public class GnomerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator gnomerAnimator;
    private static readonly int Greeting = Animator.StringToHash("isGreating");
    private static readonly int CandyClose = Animator.StringToHash("candyClose");
    private static readonly int EatingCandy = Animator.StringToHash("eatingCandy");
    private bool isEatingCandy = false;
    private bool isCandyClose = false;
    private bool isGreeting = false;

    private void Awake()
    {
        ReferenceValidator.CheckComponentForNull(ref gnomerAnimator, gameObject, nameof(gnomerAnimator), ErrorSeverity.Warning);
    }

    public bool IsEatingCandy
    {
        get => isEatingCandy;
        set
        {
            isEatingCandy = value;
            if (gnomerAnimator != null)
            {
                gnomerAnimator.SetBool(EatingCandy, isCandyClose);
            }
        }
        
    }

    public bool IsCandyClose
    {
        get => isCandyClose;
        set
        {
            isCandyClose = value;
            if (gnomerAnimator != null)
            {
                gnomerAnimator.SetBool(CandyClose, isCandyClose);
            }
        }
    }

    public bool IsGreeting
    {
        get => isGreeting;
        set
        {
            isGreeting = value;
            if (gnomerAnimator != null)
            {
                gnomerAnimator.SetBool(Greeting, isGreeting);
            }
        }
    }

    public Animator GnomerAnimator
    {
        get => gnomerAnimator;
    }
}