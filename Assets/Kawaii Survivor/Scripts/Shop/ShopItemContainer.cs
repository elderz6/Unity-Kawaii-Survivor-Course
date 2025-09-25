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

    [Header("Color")]
    [SerializeField] private Image[] levelImages;
    [SerializeField] private Outline outline;
    
    public void Configure(int level, WeaponDataSO inWeaponData)
    {
        icon.sprite = inWeaponData.Sprite;
        nameText.text = inWeaponData.Name + $" lvl {level + 1}";
        priceText.text = WeaponStatsCalculator.GetPurchasePrice(inWeaponData, level).ToString();

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;
        //multiplying the background color so the outline is brighter
        outline.effectColor = imageColor + new Color(imageColor.r * 2 ,imageColor.g * 2, imageColor.b * 2, 0.5f);
        foreach (Image image in levelImages)
            image.color = imageColor;

        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(inWeaponData, level);
        ConfigureStatContainers(calculatedStats);
    }
    
    public void Configure(ObjectDataSO objectData)
    {
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name;
        priceText.text = objectData.Price.ToString();

        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;
        //multiplying the background color so the outline is brighter
        outline.effectColor = imageColor + new Color(imageColor.r * 2 ,imageColor.g * 2, imageColor.b * 2, 0.5f);
        foreach (Image image in levelImages)
            image.color = imageColor;

        ConfigureStatContainers(objectData.BaseStats);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float>  stats)
    {
        statsContainerParent.Clear();
        StatContainerManager.GenerateStatContainers(stats, statsContainerParent);
    }
}
