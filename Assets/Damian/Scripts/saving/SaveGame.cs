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

        SaveManager.Save(data);
    }


    public static void Load()
    {
        Data data = SaveManager.Load();

        gameState.neededxp = data.neededxp;
        gameState.level = data.level;
        gameState.xp = data.xp;
    }

    public static void reset()
    {
        Data data = SaveManager.Load();

        data.level = 0;
        data.xp = 0;
        data.neededxp = 100;
    }

}
