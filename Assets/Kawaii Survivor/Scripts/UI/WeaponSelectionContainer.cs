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
    [SerializeField] private Outline outline;
    
    public void Configure(Sprite sprite, string inName, int level, WeaponDataSO inWeaponData)
    {
        icon.sprite = sprite;
        nameText.text = inName + $" lvl {level + 1}";

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;
        //multiplying the background color so the outline is brighter
        outline.effectColor = imageColor + new Color(imageColor.r * 2 ,imageColor.g * 2, imageColor.b * 2, 0.5f);
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
