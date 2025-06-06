using UnityEngine;

public static class gameState 
{
    // keeps track of the stats
    public static int level;
    public static int xp;
    public static int neededxp;
    public static int gold;


    //hard to gues whats this for
    public static Difficulty difficulty = Difficulty.Medium;


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


    
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }


    public static float GetDifficultyMultiplier()
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return 0.5f;
            case Difficulty.Medium: return 1f;
            case Difficulty.Hard: return 2f;
            default: return 1f;
        }
    }
}

