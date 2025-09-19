using UnityEngine;

public class ResourcesManager
{
   private const string statIconsDataPath = "Data/Stat Icons";

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
}
