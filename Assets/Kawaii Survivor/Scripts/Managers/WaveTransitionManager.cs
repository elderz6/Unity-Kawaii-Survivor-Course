using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    public static WaveTransitionManager instance;
    
    [Header("Elements")]
    [SerializeField] private UpgradeContainer[] upgradeContainers;
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private GameObject upgradeContainerParent;
    
    [Header("Player")]
    [SerializeField] private PlayerObjects playerObjects;
    
    [Header("Chest Settings")]
    [SerializeField] private ChestObjectContainer chestObjectContainer;
    [SerializeField] private Transform chestContainerParent;
    
    [Header("Settings")]
    private int chestsCollected;
    
    public void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.WAVETRANSITION:
                TryOpenChest();
                break;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        Chest.onCollected += ChestCollectedCallback;
    }

    private void OnDestroy()
    {
        Chest.onCollected -= ChestCollectedCallback;
    }

    [NaughtyAttributes.Button]
    private void ConfigureUpgradeContainers()
    {
        upgradeContainerParent.SetActive(true);
        foreach (UpgradeContainer upgradeContainer in upgradeContainers)
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomIndex);
            string randomStatString = Enums.FormatStatName(stat);
            Sprite upgradeSprite = ResourcesManager.GetStatIcon(stat);

            Action buttonAction = GetActionToPerform(stat, out string buttonString);
            
            upgradeContainer.Configure(upgradeSprite, randomStatString, buttonString);
            upgradeContainer.Button.onClick.RemoveAllListeners();
            upgradeContainer.Button.onClick.AddListener(() => buttonAction?.Invoke());
            upgradeContainer.Button.onClick.AddListener(BonusSelectedCallback);
        }
    }

    private void TryOpenChest()
    {
        chestContainerParent.Clear();
        if (chestsCollected > 0)
            ShowObject();
        else 
            ConfigureUpgradeContainers();
    }

    private void ShowObject()
    {
        chestsCollected--;
        
        upgradeContainerParent.SetActive(false);
        ObjectDataSO[] objectData = ResourcesManager.Objects;
        ObjectDataSO randomObject = objectData[Random.Range(0, objectData.Length)];
        
        ChestObjectContainer containerInstance = Instantiate(chestObjectContainer, chestContainerParent);
        containerInstance.Configure(randomObject);
        
        containerInstance.TakeButton.onClick.AddListener(() => TakeButtonCallback(randomObject));
        containerInstance.RecycleButton.onClick.AddListener(() => RecycleButtonCallback(randomObject));
    }

    private void TakeButtonCallback(ObjectDataSO takenObject)
    {
        playerObjects.AddObject(takenObject);
        TryOpenChest();
    }

    private void RecycleButtonCallback(ObjectDataSO recycledObject)
    {
        CurrencyManager.instance.AddCurrency(recycledObject.RecyclePrice);
        TryOpenChest();
    }

    private void BonusSelectedCallback()
    {
        GameManager.instance.WaveCompletedCallback();
    }
    
    private Action GetActionToPerform(Stat stat, out string buttonString)
    {
        float value = Random.Range(1, 10);
        buttonString = $"{Enums.FormatStatName(stat)} + {value}";
        return () => playerStatsManager.AddPlayerStats(stat, value);
    }
    
    private void ChestCollectedCallback(Chest obj)
    {
        chestsCollected++;
    }

    public bool HasCollectedChest()
    {
        return chestsCollected > 0;
    }

}
