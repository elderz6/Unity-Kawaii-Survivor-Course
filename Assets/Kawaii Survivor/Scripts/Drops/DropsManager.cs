using System;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Candy candyPrefab;

    private void Awake()
    {
        Enemy.onDie += EnemyDiedCallback;
    }

    private void OnDestroy()
    {
        Enemy.onDie -= EnemyDiedCallback;
    }

    private void EnemyDiedCallback(Vector2 enemyPos)
    {
        Instantiate(candyPrefab, enemyPos, Quaternion.identity, transform);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
