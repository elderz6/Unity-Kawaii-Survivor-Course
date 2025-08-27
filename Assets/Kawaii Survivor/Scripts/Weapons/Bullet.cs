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
    private RangedWeapon rangedWeapon;
    private Enemy target;
    private bool isCritical;
   
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
    
    public void Shoot(int inDamage, Vector2 direction, float speed, bool inIsCritical)
    {
        isCritical = inIsCritical;
        Invoke("Release", 2);
        damage = inDamage;
        transform.right = direction;
        rigidbody.linearVelocity = direction * speed;
    }
    
    public void Reload()
    {
        rigidbody.linearVelocity = Vector2.zero;
        bulletCollider.enabled = true;
        target = null;
    }

    private void Release()
    {
        if (!gameObject.activeSelf) return;
        rangedWeapon.ReleaseBullet(this);
    }
    
    private void OnTriggerEnter2D(Collider2D inCollider)
    {
        if(target != null) return;
        
        if (IsInLayerMask(inCollider.gameObject.layer, enemyMask))
        {
            target = inCollider.GetComponent<Enemy>();
            CancelInvoke("Release");
            Attack(target, isCritical);
            Release();
        }
    }

    public void Configure(RangedWeapon weapon)
    {
        rangedWeapon = weapon;
    }

    private void Attack(Enemy enemy, bool isCritical)
    {
        enemy.TakeDamage(damage, isCritical);
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
}
