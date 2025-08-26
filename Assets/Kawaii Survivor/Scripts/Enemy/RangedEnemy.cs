using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField]
    private RangedEnemyAttack attack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        attack.SetPlayer(player);
        attack.SetOwner(this);
        
        if (!hasSpawned) return;
        TryAttack();
    }

    protected override void TryAttack()
    {
        base.TryAttack();
        
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer > attackRadius)
            movement.FollowPlayer();
        else
            ManageAttack();
    }

    private void ManageAttack()
    {
        attack.AutoAim();
    }
}
