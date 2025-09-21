using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private UpgradeContainer[] upgradeContainers;
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private GameObject upgradeContainerParent;
    
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

            Action buttonAction = GetActionToPerform(stat, out string buttonString);
            
            upgradeContainer.Configure(null, randomStatString, buttonString);
            upgradeContainer.Button.onClick.RemoveAllListeners();
            upgradeContainer.Button.onClick.AddListener(() => buttonAction?.Invoke());
            upgradeContainer.Button.onClick.AddListener(BonusSelectedCallback);
        }
    }

    private void TryOpenChest()
    {
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
        Debug.Log($"Chests collected: {chestsCollected}");
    }

    private void BonusSelectedCallback()
    {
        GameManager.instance.WaveCompletedCallback();
    }
    
    private Action GetActionToPerform(Stat stat, out string buttonString)
    {
        float value = Random.Range(1, 10);
        buttonString = "+" + value;
        return () => playerStatsManager.AddPlayerStats(stat, value);
    }
    
    private void ChestCollectedCallback(Chest obj)
    {
        chestsCollected++;
    }

}
