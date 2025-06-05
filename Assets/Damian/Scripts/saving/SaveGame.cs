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
        gameState.gold = data.gold;
    }

    public static void reset()
    {
        Data data = SaveManager.Load();

        data.level = 0;
        data.xp = 0;
        data.neededxp = 100;
        data.gold = 0;
    }

}
