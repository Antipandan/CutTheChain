using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PauseImages", menuName = "ScriptableObjects/PauseImages")]
public class PauseImages : ScriptableObject
{
    [SerializeField] private Sprite restartImage;
    [SerializeField] private Sprite pauseImage;

    #region Properties

    [CanBeNull]
    public Sprite RestartImage
    {
        get => restartImage;
    }

    [CanBeNull]
    public Sprite PauseImage
    {
        get => pauseImage;
    }

    #endregion
    
}