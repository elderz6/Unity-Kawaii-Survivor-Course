using UnityEngine;
using UnityEngine.Pool;

public class RangedWeapon : Weapon
{
    [Header("Elements")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;
    
    [Header("Settings")]
    [SerializeField] private float bulletSpeed;
    
    [Header("Pooling")]
    protected ObjectPool<Bullet> bulletPool;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        bulletPool = new ObjectPool<Bullet>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    // Update is called once per frame
    protected override void Update()
    {
        AutoAim();
    }
    
    Bullet CreateFunction()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Configure(this);
        return bulletInstance;
    }
    
    private void ActionOnGet(Bullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    
    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }
    
    protected void AutoAim()
    {
        Enemy closestEnemy = FindClosestEnemy();
        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageShooting();
            return;
        }
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerpSpeed);
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        int hitDamage = GetDamage(out bool isCritical);
        
        Bullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(hitDamage, transform.up, bulletSpeed, isCritical);
    }
    
}
