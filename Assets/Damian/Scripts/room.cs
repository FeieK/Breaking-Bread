using System.Globalization;
using UnityEngine;

public static class room
{
    //enemy count
    public static float enemy1;
    public static float enemy2;
    public static float enemy3;

    //the random size bounds
    public static int minsize;
    public static int maxsize;

    //number of obj it spawns
    public static int placNumObj;
    public static bool placehubworldobj;

    public static bool unlock;

    //roomnumber
    public static int roomNum;  
    
    //failsave
    public static bool failsave;

    //bcs dont wanne change other files so much
    public static bool die;
}
