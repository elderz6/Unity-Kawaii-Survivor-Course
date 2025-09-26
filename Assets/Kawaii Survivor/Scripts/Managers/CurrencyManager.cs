using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [field: SerializeField] public int Currency { get; private set; }

    public static Action onUpdated;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateTexts();
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        UpdateTexts();
        
        onUpdated?.Invoke();
    }

    private void UpdateTexts()
    {
        CurrencyText[] texts = FindObjectsByType<CurrencyText>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (CurrencyText text in texts)
            text.UpdateText(Currency.ToString());
    }

    public bool HasEnoughCurrency(int price)
    {
        return Currency >= price;
    }

    public void SpendCurrency(int price)
    {
        AddCurrency(-price);
    }
    
    [NaughtyAttributes.Button]
    private void AddCurrencyDebug()
    {
        AddCurrency(500);
    }
}
