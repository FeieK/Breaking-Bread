using System.Collections.Generic;
using UnityEngine;

//keeps a list of the enemys to spawn them in each room
public static class EnemysEachRoom
{
    public static List<(int enemy1, int enemy2, int enemy3)> roomConfigs = new List<(int, int, int)>();

    public static void ClearConfigs()
    {
        roomConfigs.Clear();
    }

    public static void AddConfig(int enemy1, int enemy2, int enemy3)
    {
        roomConfigs.Add((enemy1, enemy2, enemy3));
    }

    public static (int, int, int) GetConfigForRoom(int roomIndex)
    {
        return roomConfigs[roomIndex];
    }
}
