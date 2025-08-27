using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Elements")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Bullet bulletPrefab;
    
    [Header("Settings")]
    [SerializeField] private float bulletSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        AutoAim();
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
        Bullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstance.Shoot(damage, transform.up, bulletSpeed);
    }
}
