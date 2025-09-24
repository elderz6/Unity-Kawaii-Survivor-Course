using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [field: SerializeField] public int Currency { get; private set; }
    
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
    }

    private void UpdateTexts()
    {
        CurrencyText[] texts = FindObjectsByType<CurrencyText>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (CurrencyText text in texts)
            text.UpdateText(Currency.ToString());
    }
}
