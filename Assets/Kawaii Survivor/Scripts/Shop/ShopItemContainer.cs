using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [field: SerializeField] public Button PurchaseButton { get; private set; }
    
    [Header("Stats")]
    [SerializeField] private Transform statsContainerParent;
    private int weaponLevel;

    [Header("Color")]
    [SerializeField] private Image[] levelImages;
    [SerializeField] private Outline outline;
    
    [Header("Lock Elements")]
    [SerializeField] private Button lockButton;
    [SerializeField] private Sprite lockedSprite, unlockedSprite;
    
    [Header("Actions")]
    public static Action<ShopItemContainer, int> onPurchased;
    
    [Header("Data")]
    public WeaponDataSO WeaponData {get; private set;}
    public ObjectDataSO ObjectData {get; private set;}
    public int purchasePrice;
    
    public bool IsLocked { get; private set; }

    private void Awake()
    {
        lockButton.onClick.AddListener(LockButtonCallback);
        CurrencyManager.onUpdated += CurrencyUpdatedCallback;
    }

    private void OnDestroy()
    {
        lockButton.onClick.RemoveAllListeners();
        CurrencyManager.onUpdated -= CurrencyUpdatedCallback;
        
    }
    
    public void Configure(int level, WeaponDataSO inWeaponData)
    {
        icon.sprite = inWeaponData.Sprite;
        nameText.text = inWeaponData.Name + $" lvl {level + 1}";
        weaponLevel = level;
        WeaponData = inWeaponData;
        
        purchasePrice = WeaponStatsCalculator.GetPurchasePrice(inWeaponData, level);
        priceText.text = purchasePrice.ToString();

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;
        //multiplying the background color so the outline is brighter
        outline.effectColor = imageColor + new Color(imageColor.r * 2 ,imageColor.g * 2, imageColor.b * 2, 0.5f);
        foreach (Image image in levelImages)
            image.color = imageColor;

        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(inWeaponData, level);
        ConfigureStatContainers(calculatedStats);

        PurchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(purchasePrice);
        PurchaseButton.onClick.AddListener(Purchase);
    }
    
    public void Configure(ObjectDataSO inObjectData)
    {
        icon.sprite = inObjectData.Icon;
        nameText.text = inObjectData.Name;
        priceText.text = inObjectData.Price.ToString();
        ObjectData = inObjectData;
        purchasePrice = inObjectData.Price;

        Color imageColor = ColorHolder.GetColor(inObjectData.Rarity);
        nameText.color = imageColor;
        //multiplying the background color so the outline is brighter
        outline.effectColor = imageColor + new Color(imageColor.r * 2 ,imageColor.g * 2, imageColor.b * 2, 0.5f);
        foreach (Image image in levelImages)
            image.color = imageColor;

        ConfigureStatContainers(inObjectData.BaseStats);
        PurchaseButton.onClick.AddListener(Purchase);
        PurchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(purchasePrice);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float>  stats)
    {
        statsContainerParent.Clear();
        StatContainerManager.GenerateStatContainers(stats, statsContainerParent);
    }

    private void LockButtonCallback()
    {
        IsLocked = !IsLocked;
        UpdateLockVisuals();
    }
    
    private void CurrencyUpdatedCallback()
    {
        PurchaseButton.interactable = CurrencyManager.instance.HasEnoughCurrency(purchasePrice);
    }

    private void UpdateLockVisuals()
    {
        lockButton.image.sprite = IsLocked ? lockedSprite : unlockedSprite;
    }

    private void Purchase()
    {
        onPurchased?.Invoke(this, weaponLevel);
    }
}
