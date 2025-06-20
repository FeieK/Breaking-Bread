using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private static string savePath => Path.Combine(Application.persistentDataPath, "Data.json");

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
        data.eqwep = gameState.eqwep;


    SaveManager.Save(data);
    }


    public static void Load()
    {
        Data data = SaveManager.Load();

        if (data.neededxp < 100)
        {
             gameState.neededxp = 100;
             gameState.pHp = 100;
             gameState.maxhp = 100;
             gameState.eqwep = 1;
            gameState.hpRecovery = 5;

}
        else
        {
            gameState.pHp = data.pHp;
            gameState.maxhp = data.maxhp;
            gameState.neededxp = data.neededxp;
            gameState.eqwep = data.eqwep;
            gameState.hpRecovery =data.hpRecovery;
        }

        gameState.xp = data.xp;
        gameState.level = data.level;

       
        //gameState.gold = 10000;
        gameState.gold = data.gold;
        gameState.unlockedWeapons = data.unlockedWeapons;

        gameState.unlockedWeapons = data.unlockedWeapons;

        gameState.unlockedUpgrades = data.unlockedUpgrades;


    }

    public static void reset()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted.");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
