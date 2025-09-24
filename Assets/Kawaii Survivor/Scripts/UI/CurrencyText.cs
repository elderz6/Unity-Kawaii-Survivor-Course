using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrencyText : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] TextMeshProUGUI currencyText;

    public void UpdateText(string currencyString)
    {
        currencyText ??= GetComponent<TextMeshProUGUI>();
        
        currencyText.text = currencyString;
    }
}
