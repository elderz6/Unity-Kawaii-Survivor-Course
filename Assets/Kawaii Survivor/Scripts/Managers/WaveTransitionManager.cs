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
            
            upgradeContainer.Configure(null, randomStatString, Random.Range(1, 10).ToString());
            upgradeContainer.Button.onClick.RemoveAllListeners();
            upgradeContainer.Button.onClick.AddListener(() => Debug.Log(randomStatString));
        }
    }
}
