using UnityEngine;

public class MeleeWeapon : Weapon
{
    private enum State
    {
        Idle,
        Attack
    }
    
    [Header("Elements")]
    [SerializeField] protected Transform hitDetectionTransform;
    [SerializeField] protected BoxCollider2D hitDetectionCollider;
    [SerializeField] protected Animator animator;
    
    private State state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        state = State.Idle;
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
    
    protected virtual void AutoAim()
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
    protected void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;

        animator.speed = 1f / attackDelay;
    }

    protected void EndAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    protected void ManageAttackTimer()
    {
        if (attackTimer > attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }

    
    protected void Attack()
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
                int hitDamage = GetDamage(out bool isCritical);
                enemy.TakeDamage(hitDamage, isCritical);
                damagedEnemies.Add(enemy);
            }
        }
    }
}
