using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class roomSelect : MonoBehaviour
{
    public GameObject map;
    public PathGrid grid;

    private int minimumE;
    private int EnemyCost1 = 1;
    private int EnemyCost2 = 2;
    private int EnemyCost3 = 3;

    private void Start()
    {
        hubworld();
    }
    private void Update()
    {
        if (room.failsave)
        {
            changemap();
        }
    }
    public void randomEnemies()
    {
        int enemy1 = 0, enemy2 = 0, enemy3 = 0;
        int totalPoints = 0;

        while (totalPoints < minimumE)
        {
            int choice = Random.Range(1, 4);

            switch (choice)
            {
                case 1:
                    enemy1++;
                    totalPoints += EnemyCost1;
                    break;
                case 2:
                    enemy2++;
                    totalPoints += EnemyCost2;
                    break;
                case 3:
                    enemy3++;
                    totalPoints += EnemyCost3;
                    break;
            }
        }

        SetEnemies(enemy1, enemy2, enemy3);
    }
    public bool SetEnemies(int enemy1, int enemy2, int enemy3)
    {
        int totalPoints = enemy1 * EnemyCost1 + enemy2 * EnemyCost2 + enemy3 * EnemyCost3;

        if (totalPoints < minimumE)
        {
            Debug.LogWarning($"Total points {totalPoints} is less than required minimum {minimumE}");
            return false;
        }

        room.enemy1 = enemy1;
        room.enemy2 = enemy2;
        room.enemy3 = enemy3;


        return true;
    }


    public void hubworld()
    {
        room.placehubworldobj = true;

        minimumE = 0;
        randomEnemies();


        room.minsize = 30;
        room.maxsize = 35;

        changemap();
    }

    public void room1()
    {
        room.placehubworldobj = false;

        minimumE = 3;
        randomEnemies();

        room.minsize = 35;
        room.maxsize = 40;

        room.placNumObj = 3;
        changemap();
    }

    public void room2()
    {
        room.placehubworldobj = false;

        minimumE = 5;
        randomEnemies();

        room.minsize = 40;
        room.maxsize = 45;

        room.placNumObj = 4;
        changemap();
    }

    public void room3()
    {
        room.placehubworldobj = false;

        minimumE = 10;
        randomEnemies();

        room.minsize = 45;
        room.maxsize = 50;

        room.placNumObj = 5;
        changemap();
    }

    public void room4()
    {
        room.placehubworldobj = false;

        minimumE = 15;
        randomEnemies();

        room.minsize = 50;
        room.maxsize = 60;

        room.placNumObj = 6;
        changemap();
    }
    public void bosroom()
    {
        room.placehubworldobj = false;

        //or 1 bos
        minimumE = 20;
        randomEnemies();

        room.minsize = 100;
        room.maxsize = 120;

        room.placNumObj = 12;
        changemap();
    }

    public void changemap()
    {
        StartCoroutine(ReloadMap());
    }

    private IEnumerator ReloadMap()
    {
        map.SetActive(false);
        //waits a frame to not break uhm everything
        yield return null;
        map.SetActive(true);
    }
}
