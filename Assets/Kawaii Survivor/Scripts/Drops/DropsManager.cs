using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class DropsManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Dictionary<Type, GameObject> prefabList = new Dictionary<Type, GameObject>();
    [SerializeField] private GameObject candyPrefab;
    [SerializeField] private GameObject cashPrefab;

    [Header("Pool")] 
    private Dictionary<Type, ObjectPool<DroppableItem>> itemPools = new Dictionary<Type, ObjectPool<DroppableItem>>();

    private void Awake()
    {
        prefabList.Add(typeof(Candy), candyPrefab);
        prefabList.Add(typeof(Cash), cashPrefab);
        
        Enemy.onDie += EnemyDiedCallback;
        Candy.onCollected += ReleaseItem;
        Cash.onCollected += ReleaseItem;
    }

    private void OnDestroy()
    {
        Enemy.onDie -= EnemyDiedCallback;
        Candy.onCollected -= ReleaseItem;
        Cash.onCollected -= ReleaseItem;
    }
  
    private void EnemyDiedCallback(Vector2 enemyPos)
    {
        bool shouldSpawnCash = Random.Range(0, 100) <= 20;
        DroppableItem droppableItem = shouldSpawnCash ? itemPools[typeof(Cash)].Get() : itemPools[typeof(Candy)].Get();
        droppableItem.transform.position = enemyPos;
        //Instantiate(droppableItem, enemyPos, Quaternion.identity, transform);
    }

    void Start()
    {
        var candyPool = new ObjectPool<DroppableItem>(CreateFunction<Candy>, ActionOnGet, ActionOnRelease, ActionOnDestroy);
        var cashPool = new ObjectPool<DroppableItem>(CreateFunction<Cash>, ActionOnGet, ActionOnRelease, ActionOnDestroy);
        itemPools.Add(typeof(Candy), candyPool);
        itemPools.Add(typeof(Cash), cashPool);
    }
    private T CreateFunction<T>() where T : Component
    {
        Type itemType = typeof(T);

        if (prefabList.TryGetValue(itemType, out GameObject prefab))
        {
            GameObject componentInstance = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            return componentInstance.GetComponent<T>();
        }
        return null;     
    }

    private void ActionOnGet<T>(T item) where T : Component
    {
        item.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease<T>(T item) where T : Component
    {
        item.gameObject.SetActive(false);
    }
    
    private void ActionOnDestroy<T>(T item) where T : Component
    {
        Destroy(item.gameObject);
    }
    
    private void ReleaseItem<T>(T item) where T : Component
    {
        Type itemType = typeof(T);
        itemPools[itemType].Release(item as DroppableItem);
    }
}
