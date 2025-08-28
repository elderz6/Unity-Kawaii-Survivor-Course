using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Colliders")] 
    [SerializeField] private CircleCollider2D mainCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectable collectable))
        {
            if (!collision.IsTouching(mainCollider))
                return;
            collectable.Collect(GetComponent<Player>());
        }
    }
}
