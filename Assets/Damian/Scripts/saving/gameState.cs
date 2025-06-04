using UnityEngine;

public static class gameState 
{
    // keeps track of the stats
    public static int level = 0;
    public static int xp = 0;
    public static int neededxp = 100;


    //dont need this for saving just to lazy to make a new scripteble script so ignore this
    public static void AddXP(int amount)
    {
        xp += amount;

        while (xp >= neededxp)
        {
            xp -= neededxp;
            level++;

            neededxp = Mathf.FloorToInt(neededxp * 1.2f);
        }
    }
}
