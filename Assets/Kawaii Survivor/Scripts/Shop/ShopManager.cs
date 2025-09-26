using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;
    [SerializeField] private Button rerollButton;
    
    private void Awake()
    {
        rerollButton.onClick.AddListener(Reroll);
    }

    private void OnDestroy()
    {
        rerollButton.onClick.RemoveAllListeners();
    }

    public void GameStateChangedCallback(GameState state)
    {
        if (state == GameState.SHOP)
            Configure();
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
    }
}
