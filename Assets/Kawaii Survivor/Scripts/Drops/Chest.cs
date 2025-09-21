using UnityEngine;
using System;
public class Chest : DroppableItem
{
    [Header("Actions")] 
    public static Action<Chest> onCollected;

    protected override void Collected()
    {
        onCollected?.Invoke(this);
        Destroy(gameObject);
    }
}