using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [field: SerializeField] public Button Button { get; private set; }

    [Header("Color")]
    [SerializeField] private Image[] levelImages;
    
    public void Configure(Sprite sprite, string inName, int level)
    {
        icon.sprite = sprite;
        nameText.text = inName;

        Color imageColor = ColorHolder.GetColor(level);

        foreach (Image image in levelImages)
            image.color = imageColor;
        
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
