using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "RestartImages", menuName = "ScriptableObjects/RestartImages")]
public class RestartImages : ScriptableObject
{
    [SerializeField] private Image restartImage;
    [SerializeField] private Image pauseImage;
    private event Action onAwake;
    [CanBeNull]
    public Image RestartImage
    {
        get => restartImage;
    }

    [CanBeNull]
    public Image PauseImage
    {
        get => pauseImage;
    }

    public event Action OnAwake
    {
        add => onAwake += value;
        remove => onAwake -= value;
    }
    
}