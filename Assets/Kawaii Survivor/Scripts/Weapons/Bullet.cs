using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Elements")] 
    private Rigidbody2D rigidbody;
    private Collider2D bulletCollider;

    [Header("Settings")] 
    [SerializeField] int damage;
    [SerializeField] private LayerMask enemyMask;
   
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bulletCollider =  GetComponent<Collider2D>();

      // LeanTween.delayedCall(gameObject, 5, () => rangedEnemyAttack.ReleaseBullet(this));
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void Shoot(int inDamage, Vector2 direction, float speed)
    {
        damage = inDamage;
        transform.right = direction;
        rigidbody.linearVelocity = direction * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D inCollider)
    {
        if (IsInLayerMask(inCollider.gameObject.layer, enemyMask))
        {
            Attack(inCollider.GetComponent<Enemy>());
            Destroy(gameObject);
        }
    }

    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
}
