using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "ScriptableObjects/New Weapon Data", order = 0)]

public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int PurchasePrice { get; private set; }
    [field: SerializeField] public Weapon Prefab { get; private set; }

    [HorizontalLine]
    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalDamage;
    [SerializeField] private float range;
    
    public Dictionary<Stat, float> BaseStats
    {
        get =>
            new()
            {
                {Stat.Attack, attack},
                {Stat.AttackSpeed, attackSpeed},
                {Stat.CriticalChance, criticalChance},
                {Stat.CriticalDamage, criticalDamage},
                {Stat.Range, range},
            };

        private set { }
    }

    public float GetStatValue(Stat stat)
    {
        if(BaseStats.ContainsKey(stat))
            return BaseStats[stat];
        return 0;
    }
}
