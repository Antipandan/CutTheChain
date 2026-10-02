using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private Image backgroundImage;
    private event Action onAmountChanged;
    private uint starsCollected = 0;
    
    public uint StarsCollected
    {
        get => starsCollected;
    }

    public int StarsCollectedInt
    {
        get => (int)starsCollected;
    }

    private void Awake()
    {
        SubscribeEvents();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        onAmountChanged += ChangeStarTotalChange;
        // detta fungerar men inte a inline:a?
        Action increment = Increment;
        if (Star.onCollectedStar == null || !Star.CollectedStarDelegates.Contains(increment)) Star.onCollectedStar += Increment;
    }

    private void UnsubscribeEvents()
    {
        Star.onCollectedStar -= Increment;
    }
    private void Increment()
    {
        starsCollected++;
        onAmountChanged?.Invoke();
    }

    private void ChangeStarTotalChange()
    {
        starsText.text = $"Stars: {starsCollected}";
    }
}
