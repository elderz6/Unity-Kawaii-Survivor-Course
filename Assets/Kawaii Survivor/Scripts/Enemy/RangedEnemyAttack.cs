using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
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
        Debug.Log("manage shooting");
        if (ownerEnemy.GetAttackTimer() >= ownerEnemy.GetAttackDelay())
        {
            Debug.Log("timer check");
            ownerEnemy.SetAttackTimer(0);
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("shooting");
        Vector2 direction = (player.transform.position - shootingPoint.position).normalized;
        gizmoDirection = direction;
    }
    
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootingPoint.position, (Vector2)shootingPoint.position + gizmoDirection * 5);
    }
    
}
