using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Transform containersParent;
    [SerializeField] private WeaponSelectionContainer weaponContainerPrefab;
    
    [Header("Data")]
    [SerializeField] private WeaponDataSO[] starterWeapons;

    public void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.WEAPONSELECTION:
                Configure();
                break;
        }
    }

    private void Configure()
    {
        containersParent.Clear();

        for (int i = 0; i < 3; i++)
        {
            GenerateWeaponContainer();
        }
    }


    private void GenerateWeaponContainer()
    {
        WeaponSelectionContainer containerInstance = Instantiate(weaponContainerPrefab, containersParent);
        WeaponDataSO weaponData = starterWeapons[Random.Range(0, starterWeapons.Length)];
        containerInstance.Configure(weaponData.Sprite, weaponData.Name);
        
        containerInstance.Button.onClick.RemoveAllListeners();
        containerInstance.Button.onClick.AddListener(() => WeaponSelectedCallback(containerInstance, weaponData));
    }

    private void WeaponSelectedCallback(WeaponSelectionContainer containerInstance, WeaponDataSO weaponData)
    {
        foreach (WeaponSelectionContainer container in containersParent.GetComponentsInChildren<WeaponSelectionContainer>())
        {
            if (container == containerInstance) container.Select();
            else container.Deselect();
        }
    }
}
