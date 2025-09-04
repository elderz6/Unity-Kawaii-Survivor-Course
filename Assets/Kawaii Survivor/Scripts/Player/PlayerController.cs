using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Elements")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private MobileJoystick mobileJoystick;
    
    [Header("Settings")]
    [SerializeField] private float baseMoveSpeed;
    private float moveSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        
    }
    
    void FixedUpdate()
    {
        rigidBody.linearVelocity = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float moveSpeedMultiplier = playerStatsManager.GetStatValue(Stat.MoveSpeed) / 100;
        moveSpeed = baseMoveSpeed * (1 +  moveSpeedMultiplier);
    }
}
