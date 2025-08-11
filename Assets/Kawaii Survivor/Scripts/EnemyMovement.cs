using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")] 
    private Player player;

    [Header("Settings")] 
    [SerializeField] private float moveSpeed;
 
    void Update()
    {
        if(player != null)
            FollowPlayer();
    }

    public void SetPlayer(Player inPlayer)
    {
        this.player = inPlayer;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
        transform.position = targetPosition;
    }
}
