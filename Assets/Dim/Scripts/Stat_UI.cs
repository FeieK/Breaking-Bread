using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stat_UI : MonoBehaviour
{
    public Slider healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set health
        healthBar.maxValue = gameState.maxhp;
        healthBar.value = gameState.pHp;
        Debug.Log(healthBar.value);
    }
}
