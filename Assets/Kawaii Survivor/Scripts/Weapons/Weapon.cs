using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour, IPlayerStatsDependency
{
    [field: SerializeField] public WeaponDataSO WeaponData { get; private set; }
    
    [Header("Attack")]
    [SerializeField] protected int damage;
    protected List<Enemy> damagedEnemies = new List<Enemy>();
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float attackTimer;
    
    [Header("Critical")]
    protected int criticalChance;
    protected float criticalDamage;
    
    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected float aimLerpSpeed;
    [SerializeField] protected LayerMask enemyMask;
    
    [Header("Level")]
    [field: SerializeField] public int Level { get; private set; }
    
    [Header("Debug")]
    [SerializeField] protected bool enableGizmos;
    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        
    }

    protected Enemy FindClosestEnemy()
    {
       Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
       
        Enemy closestEnemy = null;
        float minDistance = range;
        if (enemies.Length <= 0) return null;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy currentEnemy = enemies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, currentEnemy.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = currentEnemy;
                minDistance = distanceToEnemy;
            }
        }
        return closestEnemy;
    }
    
    protected void Wait()
    {
        attackTimer  += Time.deltaTime;
    }

    protected int GetDamage(out bool isCritical)
    {
        isCritical = false;
        if (Random.Range(0, 101) <= criticalChance)
        {
            isCritical = true;
            return Mathf.RoundToInt(damage * criticalDamage);
        }
        return damage;
    }

    protected void OnDrawGizmosSelected()
    {
        if(!enableGizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    protected void ConfigureStats()
    {
        float multiplier = 1 + (float)Level / 3;
        damage = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.Attack) * multiplier);
        attackDelay = 1f / (WeaponData.GetStatValue(Stat.AttackSpeed) * multiplier);
        criticalChance = Mathf.RoundToInt(WeaponData.GetStatValue(Stat.CriticalChance) * multiplier);
        criticalDamage = WeaponData.GetStatValue(Stat.CriticalDamage) * multiplier;

        if (WeaponData.Prefab.GetType() == typeof(RangedWeapon))
            range = WeaponData.GetStatValue(Stat.Range) * multiplier;
    }

    public void UpgradeTo(int targetLevel)
    {
        Level = targetLevel;
        ConfigureStats();
    }

    public abstract void UpdateStats(PlayerStatsManager playerStatsManager);
}
