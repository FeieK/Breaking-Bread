using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static void Save()
    {
        //cant simple call data for performence so do what u want to save in the
        //static gameState and then do as i did
        Data data = new Data();
        data.level = gameState.level;
        data.xp = gameState.xp;
        data.neededxp = gameState.neededxp;
        data.gold = gameState.gold;
        data.unlockedWeapons = gameState.unlockedWeapons;
        data.unlockedUpgrades = gameState.unlockedUpgrades;
        data.pHp = gameState.pHp;
        data.maxhp = gameState.maxhp;
        data.hpRecovery = gameState.hpRecovery;

        SaveManager.Save(data);
    }


    public static void Load()
    {
        Data data = SaveManager.Load();

        if (data.neededxp < 100)
        {
        gameState.neededxp = 100;
        }
        else
        {
            gameState.neededxp = data.neededxp;
        }

        gameState.xp = data.xp;
        gameState.level = data.level;

       
        gameState.gold = 10000;
        //gameState.gold = data.gold;
        gameState.unlockedWeapons = data.unlockedWeapons;

        gameState.unlockedWeapons = data.unlockedWeapons;

        gameState.unlockedUpgrades = data.unlockedUpgrades;
        gameState.pHp = data.pHp;
        gameState.maxhp = data.maxhp;
        gameState.hpRecovery =data.hpRecovery;

    }

    public static void reset()
    {
        Data data = SaveManager.Load();

        data.level = 0;
        data.xp = 0;
        data.neededxp = 100;
        data.gold = 0;
        data.unlockedWeapons.Clear();
        data.unlockedUpgrades.Clear();


        gameState.unlockedUpgrades.Clear();
        gameState.unlockedWeapons.Clear();
        gameState.level = 0;
        gameState.gold = 0;
        gameState.xp = 0;
        gameState.neededxp = 100;


    }

}
