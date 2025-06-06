using System.Collections.Generic;
using UnityEngine;

//keeps a list of the enemys to spawn them in each room
public static class EnemysEachRoom
{
    public static List<(int enemy1, int enemy2, int enemy3, int enemy4, int enemy5, int enemy6, int enemy7, int enemy8, int enemy9)> roomConfigs
        = new List<(int, int, int, int, int, int, int, int, int)>();

    public static void ClearConfigs()
    {
        roomConfigs.Clear();
    }

    public static void AddConfig(int enemy1, int enemy2, int enemy3, int enemy4, int enemy5, int enemy6, int enemy7, int enemy8, int enemy9)
    {
        roomConfigs.Add((enemy1, enemy2, enemy3 , enemy4, enemy5, enemy6, enemy7, enemy8, enemy9));
    }

    public static (int, int, int, int, int, int, int, int, int) GetConfigForRoom(int roomIndex)
    {
        return roomConfigs[roomIndex];
    }

}
