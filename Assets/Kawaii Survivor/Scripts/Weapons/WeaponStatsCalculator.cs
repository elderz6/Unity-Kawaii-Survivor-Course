using System.Collections.Generic;
using UnityEngine;

public class WeaponStatsCalculator
{
   public static Dictionary<Stat, float> GetStats(WeaponDataSO weaponData, int level)
   {
      float multiplier = 1 + (float)level / 3;
      Dictionary<Stat, float> newStats = new Dictionary<Stat, float>();

      foreach (KeyValuePair<Stat, float> kvp in weaponData.BaseStats)
      {
         if(kvp.Key == Stat.Range && weaponData.Prefab.GetType() != typeof(RangedWeapon))
            newStats.Add(kvp.Key, kvp.Value );
         else
            newStats.Add(kvp.Key, kvp.Value *  multiplier);
      }
      
      return newStats;
   }
}
