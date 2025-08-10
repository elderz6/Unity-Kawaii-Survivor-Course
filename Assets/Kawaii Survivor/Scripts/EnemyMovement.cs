using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")] 
    private Player player;
    
    [Header("Spawn Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;

    [Header("Settings")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRadius;
    [SerializeField] private float spawnAnimationScale;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem dieParticles;
    
    [Header("DEBUG")]
    [SerializeField] private bool gizmos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        
        if(player == null)
            Destroy(this.gameObject);
        
        SpawnAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        TryAttack();
    }

    void SpawnAnimation()
    {
        spriteRenderer.enabled = false;
        spawnIndicator.enabled = true;

        Vector3 targetScale = spawnIndicator.transform.localScale * spawnAnimationScale;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, 0.3f)
            .setLoopPingPong(4).setOnComplete(SpawnSequenceCompleted);
    }

    void SpawnSequenceCompleted()
    {
        spriteRenderer.enabled = true;
        spawnIndicator.enabled = false;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer <= attackRadius)
            PlayDeathAnimation();
    }

    private void PlayDeathAnimation()
    {
        dieParticles.transform.SetParent(null);
        dieParticles.Play();
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    
}
