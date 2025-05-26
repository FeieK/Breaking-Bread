using UnityEngine;
using System.IO;
public static class  SaveManager
{
    //dont touch it takes the data from data and puts it in a json file
    private static string savePath => Path.Combine(Application.persistentDataPath, "Data.json");

    public static void Save(Data data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public static Data Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<Data>(json);
        }
        return new Data();
    }
}
