using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestObjectContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [field: SerializeField] public Button TakeButton { get; private set; }
    [field: SerializeField] public Button RecycleButton { get; private set; }
    
    [Header("Stats")]
    [SerializeField] private Transform statsContainerParent;

    [Header("Color")]
    [SerializeField] private Image[] levelImages;
    [SerializeField] private Outline outline;

    public void Configure(ObjectDataSO objData)
    {
        icon.sprite = objData.Icon;
        nameText.text = objData.Name;
        
        Color imageColor = ColorHolder.GetColor(objData.Rarity);
        nameText.color = imageColor;
        //multiplying the background color so the outline is brighter
        outline.effectColor = imageColor + new Color(imageColor.r * 2 ,imageColor.g * 2, imageColor.b * 2, 0.5f);
        foreach (Image image in levelImages)
            image.color = imageColor;

        ConfigureStatContainers(objData.BaseStats);
    }
    
    private void ConfigureStatContainers(Dictionary<Stat, float>  calculatedStats)
    {
        StatContainerManager.GenerateStatContainers(calculatedStats, statsContainerParent);
    }
}
