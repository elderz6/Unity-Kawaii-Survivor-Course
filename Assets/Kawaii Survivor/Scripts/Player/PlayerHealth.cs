using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Settings")]
    [SerializeField] private float baseMaxHealth;
    private float maxHealth;
    private float currentHealth;
    private float armor;
    private float lifeSteal;
    private float dodge;
    private float healthRecoverySpeed;
    private float healthRecoveryTimer;
    private float healthRecoveryDuration;
    
    [Header("Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Actions")] 
    public static Action<Vector2> onAttackDodged;

    private void Awake()
    {
        Enemy.onDamageTaken += EnemyTookDamageCallback;
    }

    void Update()
    {
        if(currentHealth <= maxHealth)
            RecoverHealth();
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyTookDamageCallback;
    }
    
    private void EnemyTookDamageCallback(int damage, Vector2 enemyPos, bool bIsCritical)
    {
        if (currentHealth >= maxHealth) return;
        float lifeStealValue = damage * lifeSteal;
        float healthToAdd = Math.Min(lifeStealValue, (maxHealth - currentHealth));
        
        currentHealth += healthToAdd;
        UpdateHealthUI();
    }
    
    public void TakeDamage(int damage)
    {
        if (ShouldDodge())
        {
            onAttackDodged?.Invoke(transform.position);
            return;
        }
        
        float realDamage = damage * Mathf.Clamp( 1 - (armor / 1000),0, 10000);
        realDamage = Mathf.Min(realDamage, currentHealth); 
        currentHealth -= realDamage;
        
        UpdateHealthUI();
        if (currentHealth <= 0) Die();
    }

    private void RecoverHealth()
    {
        healthRecoveryTimer += Time.deltaTime;

        if (healthRecoveryTimer >= healthRecoveryDuration)
        {
            healthRecoveryTimer = 0;
            float healthToAdd = Mathf.Min(.1f, maxHealth - currentHealth);
            currentHealth += healthToAdd;
            UpdateHealthUI();
        }
    }

    private bool ShouldDodge()
    {
        return Random.Range(0, 100) <= dodge;
    }

    private void Die()
    {
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth/maxHealth;
        healthText.text = (int)currentHealth + " / "  + maxHealth;
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float addedHealth = playerStatsManager.GetStatValue(Stat.MaxHealth);
        maxHealth = baseMaxHealth += (int)addedHealth;
        maxHealth = Mathf.Max(maxHealth, 1);
        currentHealth = maxHealth;
        UpdateHealthUI();
        
        armor = playerStatsManager.GetStatValue(Stat.Armor);
        lifeSteal = playerStatsManager.GetStatValue(Stat.LifeSteal) / 100;
        dodge = playerStatsManager.GetStatValue(Stat.Dodge);
        healthRecoverySpeed = Mathf.Max(.001f, playerStatsManager.GetStatValue(Stat.HealthRecoverySpeed));
        healthRecoveryDuration = 1f / healthRecoverySpeed;
    }
}
