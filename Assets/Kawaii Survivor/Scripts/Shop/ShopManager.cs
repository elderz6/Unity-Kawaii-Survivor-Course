using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;
    
    [Header("Reroll")]
    [SerializeField] private Button rerollButton;
    [SerializeField] private int rerollPrice;
    [SerializeField] private TextMeshProUGUI rerollPriceText;
    
    [Header("Player Components")]
    [SerializeField] private PlayerWeapons playerWeapons;
    [SerializeField] private PlayerObjects playerObjects;
    
    
    private void Awake()
    {
        rerollButton.onClick.AddListener(Reroll);
        ShopItemContainer.onPurchased += ItemPurchasedCallback;
        CurrencyManager.onUpdated += CurrencyUpdatedCallback;
    }

    private void OnDestroy()
    {
        rerollButton.onClick.RemoveAllListeners();
        ShopItemContainer.onPurchased -= ItemPurchasedCallback;
        CurrencyManager.onUpdated -= CurrencyUpdatedCallback;
    }

    public void GameStateChangedCallback(GameState state)
    {
        if (state == GameState.SHOP)
        {
            Configure();
            UpdateRerollVisuals();
        }
    }

    private void Configure()
    {
        //containersParent.Clear();
        List<GameObject> toDestroy = new List<GameObject>();
        for (int i = 0; i < containersParent.childCount; i++)
        {
            ShopItemContainer container = containersParent.GetChild(i).GetComponent<ShopItemContainer>();
            if(!container.IsLocked)
                toDestroy.Add(container.gameObject);
        }

        while (toDestroy.Count > 0)
        {
            Transform destroyed = toDestroy[0].transform;
            destroyed.SetParent(null);
            Destroy(destroyed.gameObject);
            toDestroy.RemoveAt(0);
        }

        int shopItems = 6 - containersParent.childCount;
        int weaponContainersCount = Random.Range(Mathf.Min(2, shopItems), shopItems);
        int objectContainsCount = shopItems - weaponContainersCount;
        
        for (int i = 0; i < weaponContainersCount; i++)
        {
            ShopItemContainer weaponContainerInstance = Instantiate(shopItemContainerPrefab, containersParent);
            WeaponDataSO randomWeapon = ResourcesManager.GetRandomWeapon();
            int level = Random.Range(0, 2);
            weaponContainerInstance.Configure(level, randomWeapon);
        }
        for (int i = 0; i < objectContainsCount; i++)
        {
            ShopItemContainer objectContainerInstance = Instantiate(shopItemContainerPrefab, containersParent);
            ObjectDataSO randomObject = ResourcesManager.GetRandomObject();
            
            objectContainerInstance.Configure(randomObject);
        }
    }

    private void Reroll()
    {
        Configure();
        CurrencyManager.instance.SpendCurrency(rerollPrice);
        UpdateRerollVisuals();
    }

    private void UpdateRerollVisuals()
    {
        rerollPriceText.text = rerollPrice.ToString();
        rerollButton.interactable = CurrencyManager.instance.HasEnoughCurrency(rerollPrice);
    }

    private void CurrencyUpdatedCallback()
    {
        UpdateRerollVisuals();
    }
    
    
    private void ItemPurchasedCallback(ShopItemContainer container, int weaponLevel)
    {
        if (container.WeaponData)
            TryPurchaseWeapon(container, weaponLevel);
        else
            PurchaseObject(container);
    }

    private void TryPurchaseWeapon(ShopItemContainer container, int weaponLevel)
    {
        if (playerWeapons.TryAddWeapon(container.WeaponData, weaponLevel))
            AfterPurchase(container);
    }

    private void AfterPurchase(ShopItemContainer container)
    {
        CurrencyManager.instance.SpendCurrency(container.purchasePrice);
        Destroy(container.gameObject);
    }

    private void PurchaseObject(ShopItemContainer container)
    {
        playerObjects.AddObject(container.ObjectData);
        AfterPurchase(container);
    }

}
