using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
//for player stats
public class bufsUpgrades : MonoBehaviour
{
    [System.Serializable]
    public class UnlockEntry
    {
        public string bufName;
        public int price;
    }

    private GameObject player;
    public List<UnlockEntry> Upgradess = new List<UnlockEntry>();
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

        //acivates the unlocked upgrades
        foreach (string upgrade in gameState.unlockedUpgrades)
        {
            ActivateUpgradeScript(upgrade);
        }
    }

 

    void upgradeui()
    {
        //the ui makes buttens in a grid 
        foreach (var Upgrades in Upgradess)
        {
            GameObject btnObj = Instantiate(buttonPrefab, buttonParent);
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            Button btn = btnObj.GetComponent<Button>();

            string typeupgrade = Upgrades.UpgradesName;
            int price = Upgrades.price;

            if (gameState.unlockedUpgrades.Contains(typeupgrade))
            {
                btnText.text = $"{typeupgrade} sold";
            }
            else
            {
                btnText.text = $"{typeupgrade} - {price}g";
                // copie = smaller change it bugs
                string localType = typeupgrade;
                int localPrice = price;
                btn.onClick.AddListener(() => Buyupg(localPrice, localType, btn, btnText));
            }
        }
    }

    public void Buyupg(int goldCost, string UpgradesName, Button button, TMP_Text btnText)
    {
        //gold byebye welcome upgrade
        if (goldCost <= gameState.gold)
        {
            gameState.gold -= goldCost;
            gameState.UnlockUpgrade(UpgradesName);
            btnText.text = $"{UpgradesName}";
        }
        else
        {
            // to poor can do smt w ui here
        }
    }
}