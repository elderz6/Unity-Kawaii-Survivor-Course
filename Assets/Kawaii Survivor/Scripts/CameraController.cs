using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform target;
    [Header("Settings")]
    [SerializeField] private Vector2 minMaxXY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(target == null) return;
        
        Vector3 targetPosition = target.position;
        targetPosition.z = -10;
        
        targetPosition.x = Mathf.Clamp(targetPosition.x, -minMaxXY.x, minMaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minMaxXY.y, minMaxXY.y);
        transform.position = targetPosition;
    }
}
