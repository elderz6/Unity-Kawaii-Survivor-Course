using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Elements")] private Rigidbody2D rigidbody;
    private int damage;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
            player.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void Shoot(int inDamage, Vector2 direction, int moveSpeed)
    {
        damage = inDamage;
        transform.right = direction;
        rigidbody.linearVelocity = direction * moveSpeed;
    }
}
