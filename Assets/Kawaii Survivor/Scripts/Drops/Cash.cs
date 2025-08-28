using UnityEngine;
using System;

public class Cash : DroppableItem
{
    [Header("Actions")]
    public static Action<Cash> onCollected;
    
    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }


}
