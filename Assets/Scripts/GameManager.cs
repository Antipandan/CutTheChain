using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private event Action onFruitEaten;
    
    public event Action OnFruitEaten
    {
        add => onFruitEaten += value;
        remove => onFruitEaten -= value;
    }

    public static GameManager Instance
    {
        get => instance;
    }
    
    private void Awake()
    {
        CheckForSingleton();
    }

    private void CheckForSingleton()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    
    public void PublishOnFruitEaten()
    {
        onFruitEaten?.Invoke();
    }

}