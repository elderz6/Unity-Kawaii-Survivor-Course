using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animate(int damage, bool isCritical)
    {
        damageText.text = damage.ToString();
        damageText.color = isCritical ? Color.red : Color.white;
        
        animator.Play("DamageText");
    }
}
