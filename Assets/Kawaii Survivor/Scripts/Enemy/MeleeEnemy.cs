using UnityEngine;

public class MeleeEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!hasSpawned) return;
        movement.FollowPlayer();
        TryAttack();
    }
    
    protected override void TryAttack()
    {
        base.TryAttack();
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer <= attackRadius && attackTimer >= attackDelay)
            Attack();
        else
            Wait();
    }
}
