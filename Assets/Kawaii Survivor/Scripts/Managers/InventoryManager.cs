using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private GameObject inventoryItemContainer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Configure()
    {
        inventoryItemsParent.Clear();

        for (int i = 0; i < 10; i++)
        {
            Instantiate(inventoryItemContainer, inventoryItemsParent);
        }
    }

    public void GameStateChangedCallback(GameState state)
    {
        if(state == GameState.SHOP)
            Configure();
    }
}
