using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat_UI : MonoBehaviour
{
    // public Slider healthBar;
    public Image fillImage;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI LevelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set health
        // healthBar.maxValue = gameState.maxhp;
        // healthBar.value = gameState.pHp;
        // healthText.text = $"{gameState.pHp} / {gameState.maxhp}";
        // float t = healthBar.normalizedValue;
        // fillImage.color = Color.Lerp(Color.red, Color.green, t);

        //set gold
        // GoldText.text = $"{gameState.gold}";

        // //set Level
        // LevelText.text = $"{gameState.level}";
    }
}
