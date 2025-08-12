using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;
    void Start()
    {
        
    }

    void Update()
    {
        FindClosestEnemy();
    }

    private void FindClosestEnemy()
    {
      // Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        int closestEnemyIndex = -1;
        float minDistance = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy currentEnemy = enemies[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(transform.position, currentEnemy.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemyIndex = i;
                minDistance = distanceToEnemy;
            }
        }

        if (closestEnemyIndex == -1)
        {
            transform.up = Vector3.up;
            return;
        }
        Transform enemy = enemies[closestEnemyIndex].transform;
        
        transform.right = (enemy.position - transform.position).normalized;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
