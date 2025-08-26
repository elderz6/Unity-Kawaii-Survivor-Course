using UnityEditor;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    private Player player;
    private RangedEnemy ownerEnemy;
    
    Vector2 gizmoDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(Player inPLayer)
    {
        player = inPLayer;
    }

    public void SetOwner(RangedEnemy enemy)
    {
        ownerEnemy = enemy;
    }

    public void AutoAim()
    {
        ManageShooting();
    }

    private void ManageShooting()
    {
        ownerEnemy.SetAttackTimer(ownerEnemy.GetAttackTimer() + Time.deltaTime);
        if (ownerEnemy.GetAttackTimer() >= ownerEnemy.GetAttackDelay())
        {
            ownerEnemy.SetAttackTimer(0);
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;
        
        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Shoot(ownerEnemy.GetDamage(), direction, ownerEnemy.GetBulletSpeed());
        gizmoDirection = direction;
    }
    
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootingPoint.position, (Vector2)shootingPoint.position + gizmoDirection * 5);
    }
    
}
