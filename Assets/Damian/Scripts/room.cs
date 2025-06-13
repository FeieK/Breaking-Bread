using System.Globalization;
using UnityEngine;

public static class room
{
    //enemy count
    public static float enemy1;
    public static float enemy2;
    public static float enemy3;
    public static float enemy4;
    public static float enemy5;
    public static float enemy6;
    public static float enemy7;
    public static float enemy8;
    public static float enemy9;


    //the random size bounds
    public static int minsize;
    public static int maxsize;

    //number of obj it spawns
    public static int placNumObj;
    public static bool placehubworldobj;

    public static bool unlock;

    //roomnumber
    public static int roomNum;

    //points
    public static float runStartTime = 0f;
    public static int points = 0;

    //failsave
    public static bool failsave;

    //bcs dont wanne change other files so much
    public static bool die;
}
