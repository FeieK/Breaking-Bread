using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // it will save this
    public int level;
    public int xp;
    public int neededxp;
    public int gold;
    public List<string> unlockedWeapons = new List<string>();
    public  List<string> unlockedUpgrades = new List<string>();
    public int eqwep;

    //player
    public int pHp;
    public int maxhp;
    public int hpRecovery;

    //do anything that needs saving like this
}
