using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    //test of save and shows how to use
    public TextMeshProUGUI textMeshProUGUI;

    void Update()
    {
        textMeshProUGUI.text = "Level: " + gameState.level;
    }

    public void Save()
    {
        SaveGame.Save();  // this how we call saveing
    }

    public void Load()
    {
        SaveGame.Load();  // this for loading just in a start or awake should be enough
    }

    public void LvlUp()
    {
        gameState.level++; //this how u can change values
    }
}
