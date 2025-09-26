using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WeaponPosition[] weaponPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryAddWeapon(WeaponDataSO weapon, int level)
    {
        foreach (WeaponPosition position in weaponPositions)
        {
            if (position.Weapon) continue;
            
            position.AssignWeapon(weapon.Prefab, level);
            return true;
        }
        return false;
    }
}
