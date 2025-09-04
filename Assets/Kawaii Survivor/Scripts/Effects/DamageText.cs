using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    public void Animate(string damage, bool isCritical)
    {
        damageText.text = damage;
        damageText.color = isCritical ? Color.red : Color.white;
        
        animator.Play("DamageText");
    }
}
