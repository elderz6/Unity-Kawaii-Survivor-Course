using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image statImage;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI statValueText;

    public void Configure(Sprite icon, string statName, float statValue, bool useColor = false)
    {
        statImage.sprite = icon;
        statText.text = statName;
        
        float absStatValue = Mathf.Abs(statValue);

        Color statColor = statValue == 0 || !useColor ? Color.white : statValue > 0 ? Color.green : Color.red;
        statValueText.color = statColor;
       
        statValueText.text = absStatValue.ToString("F2");
    }

    public float GetFontSize()
    {
        return statText.fontSize;
    }

    public void SetFontSize(float fontSize)
    {
        statText.fontSizeMax = fontSize;
        statValueText.fontSizeMax = fontSize;
    }
}
