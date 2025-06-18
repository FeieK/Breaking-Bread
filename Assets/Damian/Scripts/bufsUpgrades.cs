using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
//for player stats
public class bufsUpgrades : MonoBehaviour
{
    [System.Serializable]
    public class statEntry
    {
        public string bufName;
        public int increase;
        public int price;
    }

    private GameObject player;
    public List<statEntry> stats = new List<statEntry>();
    public Transform buttonParent;
    public GameObject buttonPrefab;

    private bool lazy = true;

    void OnEnable()
    {
        if (lazy)
        {
            upgradeui();
            player = GameObject.Find("Player(Clone)");

            lazy = false;
        }
    }

    void upgradeui()
    {
        foreach (var stat in stats)
        {
            GameObject btnObj = Instantiate(buttonPrefab, buttonParent);
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            Button btn = btnObj.GetComponent<Button>();

            string localType = stat.bufName;
            int localPrice = stat.price;
            int localIncrease = stat.increase;

            btnText.text = $"{localType} - {localPrice}g";

            btn.onClick.AddListener(() => Buyupg(localPrice, localIncrease, localType, btn, btnText));
        }
    }

    public void Buyupg(int goldCost, int increase, string statName, Button button, TMP_Text btnText)
    {
        if (goldCost <= gameState.gold)
        {
            gameState.gold -= goldCost;

            // Use reflection to find and modify the stat
            var field = typeof(gameState).GetField(statName);
            if (field != null && field.FieldType == typeof(int))
            {
                int currentValue = (int)field.GetValue(null); // static field, so use null
                field.SetValue(null, currentValue + increase);

                btnText.text = $"{statName} upgraded!";
            }
            else
            {
                Debug.LogError($"Stat '{statName}' not found or not an int on gameState.");
            }
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

}