using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] protected int damage;
    protected List<Enemy> damagedEnemies = new List<Enemy>();
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float attackTimer;
    
    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected float aimLerpSpeed;
    [SerializeField] protected LayerMask enemyMask;
    
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
        if (Random.Range(0, 101) <= 50)
        {
            isCritical = true;
            return damage * 2;
        }
        return damage;
    }

    protected void OnDrawGizmosSelected()
    {
        if(!enableGizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
