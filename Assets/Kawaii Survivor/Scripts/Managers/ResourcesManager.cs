using UnityEngine;

public class ResourcesManager
{
   private const string statIconsDataPath = "Data/Stat Icons";
   private const string objectsDataPath = "Data/Objects";
   private const string weaponsDataPath = "Data/Weapons";

   private static StatIcon[] statIcons;

   public static Sprite GetStatIcon(Stat stat)
   {
      if (statIcons == null)
      {
         StatIconDataSO data = Resources.Load<StatIconDataSO>(statIconsDataPath);
         statIcons = data.StatIcons;
      }
      
      foreach (StatIcon statIcon in statIcons)
         if (statIcon.stat == stat)
            return statIcon.icon;
      
      Debug.LogError($"Could not find stat icon {stat}");
      return null;
   }


   private static ObjectDataSO[] objectData;
   
   public static ObjectDataSO[] Objects
   {
      get => objectData ??= Resources.LoadAll<ObjectDataSO>(objectsDataPath);
      private set{}
   }

   public static ObjectDataSO GetRandomObject()
   {
      return Objects[Random.Range(0, Objects.Length)];
   }
   
   private static WeaponDataSO[] weaponData;
   public static WeaponDataSO[] Weapons
   {
      get => weaponData ??= Resources.LoadAll<WeaponDataSO>(weaponsDataPath);
      private set{}
   }

   public static WeaponDataSO GetRandomWeapon()
   {
      return Weapons[Random.Range(0, Weapons.Length)];
   }
}
