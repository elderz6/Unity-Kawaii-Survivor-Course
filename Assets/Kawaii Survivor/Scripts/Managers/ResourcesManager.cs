using UnityEngine;

public class ResourcesManager
{
   private const string statIconsDataPath = "Data/Stat Icons";
   private const string objectsDataPath = "Data/Objects";

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
}
