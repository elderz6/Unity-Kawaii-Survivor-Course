using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMovement))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Components")]
    protected EnemyMovement movement;
    
    [Header("Elements")] 
    protected Player player;
    
    [Header("Spawn Elements")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    [SerializeField] protected Collider2D collider2D;
    protected bool hasSpawned = false;
    
    [Header("Settings")] 
    [SerializeField] protected float spawnAnimationScale;
    
    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    [Header("Effects")]
    [SerializeField] protected ParticleSystem dieParticles;
    
    [Header("Attack")]
    [SerializeField] protected int damage;
    [SerializeField] protected int bulletSpeed;
    [SerializeField] protected int attackRate;
    [SerializeField] protected float attackRadius;
    protected float attackDelay;
    protected float attackTimer;

    [Header("Actions")] 
    public static Action<int, Vector2> onDamageTaken;
    
    [Header("DEBUG")]
    [SerializeField] protected bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
        movement = GetComponent<EnemyMovement>();
        
        if(player == null)
            Destroy(this.gameObject);

        movement.SetPlayer(player);
        SpawnAnimation();
        attackDelay = 1f / attackRate;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.localScale = player.transform.position.x > transform.position.x ? Vector3.one : Vector3.one.With(x: -1);
    }
    
    protected void SpawnAnimation()
    {
        SetRendererVisible(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * spawnAnimationScale;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 0.3f)
            .setLoopPingPong(4).setOnComplete(SpawnSequenceCompleted);
    }

    protected void SpawnSequenceCompleted()
    {
        SetRendererVisible(true);
       
        hasSpawned = true;
        collider2D.enabled = true;
        
        movement.SetPlayer(player);
    }

    protected void Attack()
    {
        attackTimer = 0f;
        player.TakeDamage(damage);
    }

    protected void Wait()
    {
        attackTimer +=  Time.deltaTime;
    }
    
    protected void PlayDeathAnimation()
    {
        dieParticles.transform.SetParent(null);
        dieParticles.Play();
        Destroy(this.gameObject);
    }

    protected void SetRendererVisible(bool visible)
    {
        spriteRenderer.enabled = visible;
        spawnIndicator.enabled = !visible;
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, currentHealth); 
        currentHealth -= realDamage;
        onDamageTaken?.Invoke(damage, transform.position);
        
        if (currentHealth <= 0)
        {
            PlayDeathAnimation();
        }
    }

    protected virtual void TryAttack()
    {
    }

    protected void OnDrawGizmos()
    {
        if (!gizmos) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public int GetAttackRate()
    {
        return attackRate;
    }

    public float GetAttackRadius()
    {
        return attackRadius;
    }

    public float GetAttackDelay()
    {
        return attackDelay;
    }
    
    public float GetAttackTimer()
    {
        return attackTimer;
    }
    public int GetDamage()
    {
        return damage;
    }
    public int GetBulletSpeed()
    {
        return bulletSpeed;
    }
    public void SetAttackTimer(float newTimer)
    {
        attackTimer = newTimer;
    }

  
}
