using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using TMPro;
using Utility;

public class PercentageHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI percentageText;

    private void Awake()
    {
        CheckReferences();       
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref percentageText, gameObject, nameof(percentageText));
    }

    /// <summary>
    /// Takes a number of percentage and makes an output. Max percentage will only be 100% and minimum will be 0%
    /// </summary>
    /// <param name="percentage">Percentage to configure text with</param>
    /// <param name="decimals">How many decimals? Zero is standard</param>
    public void ChangePercentageScaled(float percentage, uint decimals = 0)
    {
        float scaledPercentage = Mathf.Clamp(percentage, 0f, 100f);
        ChangePercentageUnScaled(scaledPercentage, decimals);
    }

    public void ChangePercentageUnScaled(float percentage, uint decimals = 0)
    {
        if (percentageText != null) percentageText.text = Math.Round(percentage, (int)decimals).ToString(CultureInfo.InvariantCulture);
    }
}