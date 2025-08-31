
public enum GameState
{
    MENU,
    WEAPONSELECTION,
    GAME,
    GAMEOVER,
    STAGECOMPLETE,
    WAVETRANSITION,
    SHOP
}


public enum Stat
{
    Attack,
    AttackSpeed,
    CriticalChance,
    CriticalDamage,
    MoveSpeed,
    MaxHealth,
    Range,
    HealthRecoverySpeed,
    Armor,
    Luck,
    Dodge,
    LifeSteal
}

public static class Enums
{
    public static string FormatStatName(Stat stat)
    {
        string unformatted = stat.ToString();
        string formatted = "";

        for (int i = 0; i < unformatted.Length; i++)
        {
            if (i == 0)
            {
                formatted += unformatted[i];
                continue;
            }
            if (char.IsUpper(unformatted[i]))
                formatted += " ";
            formatted += unformatted[i];
        }
        
        return formatted;
    }
}