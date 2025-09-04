using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterDataSO playerData;
    
    [Header("Stats")] 
    private Dictionary<Stat, float> addends = new Dictionary<Stat, float>();
    private Dictionary<Stat, float> playerStats = new Dictionary<Stat, float>();

    private void Awake()
    {
        playerStats = playerData.BaseStats;

        foreach (KeyValuePair<Stat, float> kvp in playerStats)
            addends.Add(kvp.Key, 0);
    }

    void Start()
    {
        UpdatePlayerStats();
    }

    public void AddPlayerStats(Stat stat, float value)
    {
        if(addends.ContainsKey(stat)) 
            addends[stat] += value;
        
        UpdatePlayerStats();
    }

    public float GetStatValue(Stat stat)
    {
        float value = playerStats[stat] + addends[stat];
        return value;
    }

    private void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsDependency> playerStatsDependencies =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .OfType<IPlayerStatsDependency>();
        
        foreach (IPlayerStatsDependency playerStatsDependency in playerStatsDependencies)
            playerStatsDependency.UpdateStats(this);
    }
}
