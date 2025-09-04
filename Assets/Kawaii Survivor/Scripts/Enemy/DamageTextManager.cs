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
        PlayerHealth.onAttackDodged += AttackDodgedCallback;
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallback;
        PlayerHealth.onAttackDodged -= AttackDodgedCallback;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private void ShowDamageText(string damage, Vector2 position, bool isCritical)
    {
        DamageText damageTextIntance = damageTextPool.Get();
        Vector3 spawnPosition = position + Vector2.up * 1.5f;
        damageTextIntance.transform.position = spawnPosition;

        damageTextIntance.Animate(damage, isCritical);

        LeanTween.delayedCall(1, () => damageTextPool.Release(damageTextIntance));
    }

    private void EnemyHitCallback(int damage, Vector2 enemyPosition, bool isCritical)
    {
        ShowDamageText(damage.ToString(), enemyPosition, isCritical);
    }
    
    private void AttackDodgedCallback(Vector2 playerPosition)
    {
        ShowDamageText("Dodged", playerPosition, false);
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
