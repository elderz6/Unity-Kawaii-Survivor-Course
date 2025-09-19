using System.Collections.Generic;
using UnityEngine;

public class StatContainerManager : MonoBehaviour
{
    public static StatContainerManager instance;
    [Header("Elements")]
    [SerializeField] private StatContainer statContainer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }


    private void GenerateContainers(Dictionary<Stat, float> statDictionary, Transform parent)
    {
        foreach (KeyValuePair<Stat, float> kvp in statDictionary)
        {
            StatContainer containerInstance = Instantiate(statContainer, parent);

            Sprite weaponIcon = ResourcesManager.GetStatIcon(kvp.Key);
            string statName = Enums.FormatStatName(kvp.Key);
            string statValue = kvp.Value.ToString();
            
            containerInstance.Configure(weaponIcon, statName, statValue);
        }
    }

    public static void GenerateStatContainers(Dictionary<Stat, float> statDictionary, Transform parent)
    {
        instance.GenerateContainers(statDictionary, parent);
    }
}
