using UnityEngine;
using System;
public class Chest : MonoBehaviour
{
    [Header("Actions")] 
    public static Action onColleted;

    public void Collect(Player player)
    {
        onColleted?.Invoke();
        Destroy(gameObject);
    }
}
