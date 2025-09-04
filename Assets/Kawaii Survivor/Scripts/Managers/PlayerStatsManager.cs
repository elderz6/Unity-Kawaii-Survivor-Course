using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Stats")] 
    private Dictionary<Stat, float> addends = new Dictionary<Stat, float>();

    private void Awake()
    {
        addends.Add(Stat.MaxHealth, 10);
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
        return addends[stat];
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
