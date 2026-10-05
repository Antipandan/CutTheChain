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

    #region Properties

    public uint StarsCollected
    {
        get => starsCollected;
    }

    public int StarsCollectedInt
    {
        get => (int)starsCollected;
    }

    #endregion

    #region Unity Lifecycle

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

    #endregion

    #region Custom Methods

    private void SubscribeEvents()
    {
        onAmountChanged -= ChangeStarTotalChange;
        onAmountChanged += ChangeStarTotalChange;
        Star.onCollectedStar -= Increment;
        Star.onCollectedStar += Increment;
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

    #endregion

}
