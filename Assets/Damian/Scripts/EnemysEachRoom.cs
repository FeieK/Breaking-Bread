using System.Collections.Generic;
using UnityEngine;

//keeps a list of the enemys to spawn them in each room
public static class EnemysEachRoom
{
    public static List<int[]> roomConfigs = new List<int[]>();

    public static void ClearConfigs()
    {
        roomConfigs.Clear();
    }

    public static void AddConfig(int[] enemies)
    {
        roomConfigs.Add((int[])enemies.Clone()); 
    }

    public static int[] GetConfigForRoom(int roomIndex)
    {
        return roomConfigs[roomIndex];
    }
}
