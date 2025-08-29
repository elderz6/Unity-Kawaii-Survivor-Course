using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxHealth;
    private int currentHealth;
    
    [Header("Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, currentHealth); 
        currentHealth -= realDamage;
        
        UpdateHealthUI();
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = (float)currentHealth/maxHealth;
        healthText.text = currentHealth + " / "  + maxHealth;;
    }
}
