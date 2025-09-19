using UnityEngine;
using System;
public class Chest : DroppableItem
{
    [Header("Actions")] 
    public static Action onColleted;

    public void Collect(Player player)
    {
        onColleted?.Invoke();
        Destroy(gameObject);
    }

    protected override void Collected()
    {
        throw new NotImplementedException();
    }
}