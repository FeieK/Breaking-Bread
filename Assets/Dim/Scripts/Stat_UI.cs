using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat_UI : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LevelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //set gold
         GoldText.text = $"{gameState.gold}";

        //set Level
        LevelText.text = $"{gameState.level}";
    }
}
