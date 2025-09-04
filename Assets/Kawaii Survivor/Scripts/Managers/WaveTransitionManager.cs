using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private UpgradeContainer[] upgradeContainers;
    [SerializeField] private PlayerStatsManager playerStatsManager;
    
    public void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeContainers();
                break;
        }
    }

    [NaughtyAttributes.Button]
    private void ConfigureUpgradeContainers()
    {
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
}
