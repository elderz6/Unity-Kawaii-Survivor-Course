using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Button[] upgradeContainers;
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
        foreach (Button upgradeContainer in upgradeContainers)
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);
            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomIndex);
            string randomStatString = Enums.FormatStatName(stat);
            
            upgradeContainer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomStatString;
            upgradeContainer.onClick.RemoveAllListeners();
            upgradeContainer.onClick.AddListener(() => Debug.Log(randomStatString));
        }
    }
}
