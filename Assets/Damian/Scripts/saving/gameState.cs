using System.Collections.Generic;
using UnityEngine;

public static class gameState 
{
    // keeps track of the stats
    public static int level;
    public static int xp;
    public static int neededxp;
    public static int gold;
    public static int eqwep;
    public static List<string> unlockedWeapons = new List<string>();
    public static List<string> unlockedUpgrades = new List<string>();
    //player
    public static int pHp;
    public static int maxhp;
    public static int hpRecovery;




    //hard to gues whats this for
    public static Difficulty difficulty = Difficulty.Medium;


    //dont need this for saving just to lazy to make a new scripteble script so ignore this and handy to place it here

    public static void UnlockWeapon(string weaponName)
    {
        if (!unlockedWeapons.Contains(weaponName))
        {
            unlockedWeapons.Add(weaponName);
        }
    }

    public static void UnlockUpgrade(string weaponName)
    {
        if (!unlockedWeapons.Contains(weaponName))
        {
            unlockedWeapons.Add(weaponName);
        }
    }

    public static void AddXP(int amount)
    {
        xp += amount;

        while (xp >= neededxp)
        {
            xp -= neededxp;
            level++;

            neededxp = Mathf.FloorToInt(neededxp * 1.2f);
        }
    }


    
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }


    public static float GetDifficultyMultiplier()
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return 0.5f;
            case Difficulty.Medium: return 1f;
            case Difficulty.Hard: return 2f;
            default: return 1f;
        }
    }
}

