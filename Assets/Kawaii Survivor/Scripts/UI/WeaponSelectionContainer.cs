using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [field: SerializeField] public Button Button { get; private set; }
    
    [Header("Stats")]
    [SerializeField] private Transform statsContainerParent;

    [Header("Color")]
    [SerializeField] private Image[] levelImages;
    
    public void Configure(Sprite sprite, string inName, int level, WeaponDataSO inWeaponData)
    {
        icon.sprite = sprite;
        nameText.text = inName + $" lvl {level + 1}";

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;

        foreach (Image image in levelImages)
            image.color = imageColor;

        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(inWeaponData, level);
        ConfigureStatContainers(calculatedStats);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float>  calculatedStats)
    {
        StatContainerManager.GenerateStatContainers(calculatedStats, statsContainerParent);
    }

    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.075f, .3f).setEase(LeanTweenType.easeInOutSine);
    }

    public void Deselect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, .3f);
    }
}
