using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class wepShop : MonoBehaviour
{

    [System.Serializable]
    public class WeaponEntry
    {
        public string weaponName;
        public int price;
        public GameObject prefab;
    }


    public List<WeaponEntry> weapons = new List<WeaponEntry>();
    public Transform buttonParent;
    public GameObject buttonPrefab;
    private Transform wepspawnloc;

    //dont use u dont need this!!!
    private bool lazy = true;
    void OnEnable()
    {
        if (lazy) 
        { 
            ShopUI();
            lazy = false;
        }
    }

    //same logic as upgradeShop see those coments
    private void Update()
    {
        if (room.placehubworldobj)
        {
            if (wepspawnloc == null)
                wepspawnloc = GameObject.FindWithTag("wepSpawnLocation").transform;
        }
    }

    void ShopUI()
    {
        foreach (var weapon in weapons)
        {
            GameObject btnObj = Instantiate(buttonPrefab, buttonParent);
            TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
            Button btn = btnObj.GetComponent<Button>();

            string wepname = weapon.weaponName;
            int price = weapon.price;

            if (gameState.unlockedWeapons.Contains(wepname))
            {
                btnText.text = $"{wepname}";
                btn.onClick.AddListener(() => spawnwep(wepname));

            }
            else
            {
                btnText.text = $"{wepname} - {price}g";
                btn.onClick.AddListener(() => Buy(price, wepname, btn, btnText));
            }
        }
    }

    public void Buy(int goldCost, string weaponName, Button button, TMP_Text btnText)
    {

        if (goldCost <= gameState.gold)
        {
            gameState.gold -= goldCost;
            gameState.UnlockWeapon(weaponName);
            btnText.text = $"{weaponName}";
            spawnwep(weaponName);
            SaveGame.Save();
        }
        else
        {
            //to poor can do smt w ui here
        }
    }


    public void spawnwep(string weaponName)
    {
        WeaponEntry weapon = weapons.Find(w => w.weaponName == weaponName);

        if (weapon == null)
        {
            Debug.LogError($"Weapon '{weaponName}' not found in weapons list!");
            return;
        }

        if (weapon.prefab == null)
        {
            Debug.LogError($"Prefab for weapon '{weaponName}' is not assigned!");
            return;
        }

        if (wepspawnloc == null)
        {
            Debug.LogError("wepspawnloc is not assigned!");
            return;
        }

        Instantiate(weapon.prefab, wepspawnloc.position, Quaternion.identity, this.transform);
    }

}
