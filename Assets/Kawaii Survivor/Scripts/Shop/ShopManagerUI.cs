using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [Header("Stats Elements")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private GameObject playerStatsBackground;
    private Vector2 playerStatsOpenPos;
    private Vector2 playerStatsClosedPos;
    
    [Header("Inventory Elements")]
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private GameObject inventoryBackground;
    private Vector2 inventoryOpenPos;
    private Vector2 inventoryClosedPos;
    
    [Header("Item Info Elements")]
    [SerializeField] private RectTransform itemInfoPanel; 
    private Vector2 itemInfoOpenPos;
    private Vector2 itemInfoClosedPos;

    IEnumerator Start()
    {
        yield return null;
        ConfigureStatsPanel();
        ConfigureInventoryPanel();
        ConfigureItemInfoPanel();
    }

    private void ConfigureItemInfoPanel()
    {
        float height = Screen.height / (2 * itemInfoPanel.lossyScale.x);
        itemInfoPanel.offsetMax = itemInfoPanel.offsetMax.With(y: height);

        itemInfoOpenPos = itemInfoPanel.anchoredPosition;
        itemInfoClosedPos = itemInfoOpenPos + Vector2.down * height;
        
        itemInfoPanel.anchoredPosition = itemInfoClosedPos;
        itemInfoPanel.gameObject.SetActive(false);
    }

    [NaughtyAttributes.Button]
    public void ShowItemInfo()
    {
        itemInfoPanel.gameObject.SetActive(true);
        itemInfoPanel.LeanCancel();
        itemInfoPanel.LeanMove((Vector3)itemInfoOpenPos, .3f).setEase(LeanTweenType.easeOutCubic);
    }

    [NaughtyAttributes.Button]
    public void HideItemInfo()
    {
        itemInfoPanel.LeanCancel();
        itemInfoPanel.LeanMove((Vector3)itemInfoClosedPos, .3f).setEase(LeanTweenType.easeInCubic)
            .setOnComplete(() => itemInfoPanel.gameObject.SetActive(false));
    }

    private void ConfigureStatsPanel()
    {
        float width = Screen.width / (4 * playerStatsPanel.lossyScale.x);
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x: width);

        playerStatsOpenPos = playerStatsPanel.anchoredPosition;
        playerStatsClosedPos = playerStatsOpenPos - Vector2.right * width;

        playerStatsPanel.anchoredPosition = playerStatsClosedPos;
        HidePlayerStats();
    }

    public void ShowPlayerStats()
    {
        playerStatsPanel.gameObject.SetActive(true);
        playerStatsBackground.SetActive(true);
        playerStatsBackground.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsOpenPos, .3f).setEase(LeanTweenType.easeInCubic);
    }

    public void HidePlayerStats()
    {
        playerStatsBackground.SetActive(false);
        playerStatsBackground.GetComponent<Image>().raycastTarget = false;
        
        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsClosedPos, .3f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => playerStatsPanel.gameObject.SetActive(false));
    }

    private void ConfigureInventoryPanel()
    {
        float width = Screen.width / (4 * inventoryPanel.lossyScale.x);
        inventoryPanel.offsetMin = inventoryPanel.offsetMin.With(x: -width);

        inventoryOpenPos = inventoryPanel.anchoredPosition;
        inventoryClosedPos = inventoryOpenPos - Vector2.left * width;

        inventoryPanel.anchoredPosition = inventoryClosedPos;
        HideInventory(false);
    }

    public void HideInventory(bool hideItem)
    {
        inventoryBackground.SetActive(false);
        inventoryBackground.GetComponent<Image>().raycastTarget = false;
        
        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, inventoryClosedPos, .3f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => inventoryPanel.gameObject.SetActive(false));

        if (hideItem) HideItemInfo();
    }

    public void ShowInventory()
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryBackground.SetActive(true);
        inventoryBackground.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(inventoryPanel);
        LeanTween.move(inventoryPanel, inventoryOpenPos, .3f).setEase(LeanTweenType.easeInCubic);
    }
}
