using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class RangedEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    private Player player;
    private RangedEnemy ownerEnemy;
    
    [Header("Pooling")]
    private ObjectPool<EnemyBullet> bulletPool;
    
    Vector2 gizmoDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletPool = new ObjectPool<EnemyBullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }
    
    EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Configure(this);
        return bulletInstance;
    }
    
    private void ActionOnGet(EnemyBullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    
    private void ActionOnDestroy(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void ReleaseBullet(EnemyBullet bullet)
    {
        bulletPool.Release(bullet);
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

        EnemyBullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(ownerEnemy.GetDamage(), direction, ownerEnemy.GetBulletSpeed());
        gizmoDirection = direction;
    }
    
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootingPoint.position, (Vector2)shootingPoint.position + gizmoDirection * 5);
    }
    
}
