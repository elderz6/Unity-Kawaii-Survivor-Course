using System;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Elements")]
    [SerializeField] private Transform playerStatsContainerParent;
    
    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        int index = 0;
        foreach (Stat stat in Enum.GetValues(typeof(Stat)))
        {
            StatContainer statContainer = playerStatsContainerParent.GetChild(index).GetComponentInChildren<StatContainer>();
            statContainer.gameObject.SetActive(true);

            Sprite statIcon = ResourcesManager.GetStatIcon(stat);
            string statValue = playerStatsManager.GetStatValue(stat).ToString("F2");
            
            statContainer.Configure(statIcon, Enums.FormatStatName(stat), statValue);
            
            index++;
        }
    }
}
