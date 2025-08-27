using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private DamageText damageTextPrefab;
    
    [Header("Pooling")]
    private ObjectPool<DamageText> damageTextPool;
    
    private void Awake()
    {
        Enemy.onDamageTaken += EnemyHitCallback;
    }

    private void onDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallback;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyHitCallback(int damage, Vector2 enemyPosition, bool isCritical)
    {
        DamageText damageTextIntance = damageTextPool.Get();
        Vector3 spawnPosition = enemyPosition + Vector2.up * 1.5f;
        damageTextIntance.transform.position = spawnPosition;

        damageTextIntance.Animate(damage, isCritical);

        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextIntance));
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(DamageText damageTextInstance)
    {
        damageTextInstance.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageText damageTextInstance)
    {
        damageTextInstance.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText damageTextInstance)
    {
        Destroy(damageTextInstance.gameObject);
    }
}
