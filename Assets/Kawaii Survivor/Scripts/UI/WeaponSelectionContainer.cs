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
    [SerializeField] private StatContainer statContainerPrefab;
    //private WeaponDataSO weaponData;

    [Header("Color")]
    [SerializeField] private Image[] levelImages;
    
    public void Configure(Sprite sprite, string inName, int level, WeaponDataSO inWeaponData)
    {
        icon.sprite = sprite;
        nameText.text = inName;

        Color imageColor = ColorHolder.GetColor(level);

        foreach (Image image in levelImages)
            image.color = imageColor;
        ConfigureStatContainers(inWeaponData);
    }

    private void ConfigureStatContainers(WeaponDataSO weaponData)
    {
        foreach (KeyValuePair<Stat, float> kvp in weaponData.BaseStats)
        {
            StatContainer containerInstance = Instantiate(statContainerPrefab, statsContainerParent);

            Sprite weaponIcon = ResourcesManager.GetStatIcon(kvp.Key);
            string statName = Enums.FormatStatName(kvp.Key);
            string statValue = kvp.Value.ToString();
            
            containerInstance.Configure(weaponIcon, statName, statValue);
        }
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
