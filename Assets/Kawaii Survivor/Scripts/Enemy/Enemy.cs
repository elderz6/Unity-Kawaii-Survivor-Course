using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    private EnemyMovement movement;
    
    [Header("Elements")] 
    private Player player;
    
    [Header("Spawn Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;
    
    [Header("Settings")] 
    [SerializeField] private float spawnAnimationScale;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem dieParticles;
    
    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private int attackRate;
    [SerializeField] private float attackRadius;
    private float attackDelay;
    private float attackTimer;
    
    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        movement = GetComponent<EnemyMovement>();
        
        if(player == null)
            Destroy(this.gameObject);

        SpawnAnimation();
        attackDelay = 1f / attackRate;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned) return;
        TryAttack();
    }
    
    void SpawnAnimation()
    {
        SetRendererVisible(false);

        Vector3 targetScale = spawnIndicator.transform.localScale * spawnAnimationScale;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 0.3f)
            .setLoopPingPong(4).setOnComplete(SpawnSequenceCompleted);
    }

    void SpawnSequenceCompleted()
    {
        SetRendererVisible(true);
       
        hasSpawned = true;
        
        movement.SetPlayer(player);
    }
    
    
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer <= attackRadius && attackTimer >= attackDelay)
            Attack();
        else
            Wait();
    }

    private void Attack()
    {
        attackTimer = 0f;
        player.TakeDamage(damage);
    }

    private void Wait()
    {
        attackTimer +=  Time.deltaTime;
    }
    
    private void PlayDeathAnimation()
    {
        dieParticles.transform.SetParent(null);
        dieParticles.Play();
        Destroy(this.gameObject);
    }

    private void SetRendererVisible(bool visible)
    {
        spriteRenderer.enabled = visible;
        spawnIndicator.enabled = !visible;
    }
    
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
