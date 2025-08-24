using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    enum State
    {
        Idle,
        Attack
    }
    
    private State state;
    
    [Header("Elements")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private BoxCollider2D hitDetectionCollider;
    [SerializeField] private Animator animator;
    
    [Header("Attack")]
    [SerializeField] private int damage;
    private List<Enemy> damagedEnemies = new List<Enemy>();
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackTimer;
    
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private float hitDetectionRadius;
    [SerializeField] private float aimLerpSpeed;
    [SerializeField] private LayerMask enemyMask;
    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    private Enemy FindClosestEnemy()
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

    private void AutoAim()
    {
        Enemy closestEnemy = FindClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            ManageAttackTimer();
            transform.right = (closestEnemy.transform.position - transform.position).normalized;
            targetUpVector = transform.up;
        }
        
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerpSpeed);
        Wait();
    }

    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;

        animator.speed = 1f / attackDelay;
    }

    private void Attacking()
    {
        Attack();
    }
    
    private void EndAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    private void ManageAttackTimer()
    {
        if (attackTimer > attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }

    private void Wait()
    {
        attackTimer  += Time.deltaTime;
    }

    private void Attack()
    {
        //Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDetectionRadius, enemyMask);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(
            hitDetectionTransform.position, 
            hitDetectionCollider.bounds.size,
            hitDetectionTransform.localEulerAngles.z,
            enemyMask);
        
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            if (!damagedEnemies.Contains(enemy))
            {
                enemy.TakeDamage(damage);
                damagedEnemies.Add(enemy);
            }
        }
    }


    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDetectionRadius);
    }
}
