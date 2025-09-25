using UnityEngine;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private ShopItemContainer shopItemContainerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStateChangedCallback(GameState state)
    {
        if (state == GameState.SHOP)
            Configure();
    }

    private void Configure()
    {
        containersParent.Clear();

        int shopItems = 6;
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
}
