using UnityEngine;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private GameObject shopItemContainerPrefab;

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
        for (int i = 0; i < shopItems; i++)
        {
            Instantiate(shopItemContainerPrefab, containersParent);
        }
    }
}
