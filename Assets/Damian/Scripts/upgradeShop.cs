using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UpgradeShop : MonoBehaviour
{
    [System.Serializable]
    public class UnlockEntry
    {
        public string UpgradesName;
        public int price;
    }

    private GameObject player;
    public List<UnlockEntry> Upgradess = new List<UnlockEntry>();
    public Transform buttonParent;
    public GameObject buttonPrefab;

    void OnEnable()
    {
        upgradeui();
        player = GameObject.Find("Player(Clone)");

        //acivates the unlocked upgrades
        foreach (string upgrade in gameState.unlockedUpgrades)
        {
            ActivateUpgradeScript(upgrade);
        }
    }

    void ActivateUpgradeScript(string upgradeName)
    {
        //looks for the name then searches that script on player

        UnlockEntry entry = Upgradess.Find(u => u.UpgradesName == upgradeName);

        if (entry != null)
        {
            var scriptInstance = player.GetComponents<MonoBehaviour>()
                .FirstOrDefault(s => s.GetType().Name == upgradeName);

            if (scriptInstance != null)
            {
                Debug.Log($"Found script '{upgradeName}'");
                scriptInstance.enabled = true;
            }
            else
            {
                Debug.LogWarning($"failed'{upgradeName}'");
            }
        }
        else
        {
            Debug.LogWarning($"failed '{upgradeName} the second");
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
            ActivateUpgradeScript(UpgradesName);
        }
        else
        {
            // to poor can do smt w ui here
        }
    }
}
