using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider expBar;

    [Header("Settings")] 
    private int requiredEXP;
    private int currentEXP;
    private int level = 1;

    private void Awake()
    {
        Candy.onCollected += CandyCollectedCallback;
    }

    private void OnDestroy()
    {
        Candy.onCollected -= CandyCollectedCallback;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateRequiredEXP();
        UpdateVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateRequiredEXP()
    {
        requiredEXP = level * 5;
    }

    private void UpdateVisuals()
    {
        expBar.value = (float)currentEXP / requiredEXP;
        levelText.text = "Lv. " + level;
    }

    private void LevelUp()
    {
        level++;
        currentEXP = 0;
        UpdateRequiredEXP();
    }

    private void CandyCollectedCallback(Candy candy)
    {
        currentEXP++;

        if (currentEXP >= requiredEXP)
            LevelUp();
        
        UpdateVisuals();
    }
}
