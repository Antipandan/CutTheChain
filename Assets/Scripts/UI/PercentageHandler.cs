using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using TMPro;
using Unity.Mathematics;
using CustomUtility;

public class PercentageHandler : MonoBehaviour
{
    [SerializeField] [Range(0, 10)]private int decimals = 1;
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
    /// <param name="numberDecimals">How many decimals? Zero is standard</param>
    public void ChangePercentageScaled(float percentage, uint numberDecimals = 0)
    {
        float scaledPercentage = Mathf.Clamp(percentage, 0f, 100f);
        ChangePercentageUnScaled(scaledPercentage, numberDecimals);
    }

    /// <summary>
    /// Takes a number of percentage and makes an output
    /// </summary>
    /// <param name="percentage">Percentage to configure text with</param>
    /// <param name="numberDecimals">How many decimal? Zero is standard</param>
    public void ChangePercentageUnScaled(float percentage, uint numberDecimals = 0)
    {
        if (percentageText != null) percentageText.text = $"{Math.Round(percentage, (int)numberDecimals).ToString(CultureInfo.InvariantCulture)}%";
    }

    /// <summary>
    /// Percentage in decimal where 1 is 100% and 0 is 0%
    /// </summary>
    /// <param name="percentage">decimal</param>
    public void ChangePercentage(float percentage)
    {
        if (percentageText != null) percentageText.text = $"{Math.Round(percentage * 100, decimals).ToString(CultureInfo.InvariantCulture)}%";
    }
}