using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Elements")] private Rigidbody2D rigidbody;
    private int damage;
    private RangedEnemyAttack rangedEnemyAttack;
    private Collider2D bulletCollider;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bulletCollider =  GetComponent<Collider2D>();

        LeanTween.delayedCall(gameObject, 5, () => rangedEnemyAttack.ReleaseBullet(this));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            LeanTween.cancel(gameObject);
            player.TakeDamage(1);
            bulletCollider.enabled = false;
            rangedEnemyAttack.ReleaseBullet(this);
        }
    }

    public void Shoot(int inDamage, Vector2 direction, int moveSpeed)
    {
        damage = inDamage;
        transform.right = direction;
        rigidbody.linearVelocity = direction * moveSpeed;
    }
    
    
    public void Configure(RangedEnemyAttack inRangedAttack)
    {
        rangedEnemyAttack = inRangedAttack;
    }

    public void Reload()
    {
        rigidbody.linearVelocity = Vector2.zero;
        bulletCollider.enabled = true;
    }

}
