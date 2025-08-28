using System.Collections;
using UnityEngine;

public abstract class DroppableItem : MonoBehaviour, ICollectable
{
    private bool collected = false;

    private void OnEnable()
    {
        collected = false;
    }
    
    public void Collect(Player player)
    {
        if (collected) return;
        collected = true;
        
        StartCoroutine(MoveTowardsPlayer(player.transform));
    }

    IEnumerator MoveTowardsPlayer(Transform playerTransform)
    {
        float timer = 0;
        Vector2 startPosition = transform.position;
        
        while (timer < 1)
        {
            transform.position = Vector2.Lerp(startPosition, playerTransform.position, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        Collected();
    }

    protected abstract void Collected();

}
