using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roomSelect : MonoBehaviour
{
    public GameObject map;
    public PathGrid grid;
    public bool random = true;

    int roomNum = 1;

    private int minimumE;
    private int EnemyCost1 = 1;
    private int EnemyCost2 = 2;
    private int EnemyCost3 = 3;

    //the ui
    public GameObject enemyui;
    public TextMeshPro minEnScore;
    public TextMeshPro totaleEnScore;
    public Button button;

    //the enemies
    public Dropdown dropdownEnemy1;
    public Dropdown dropdownEnemy2;
    public Dropdown dropdownEnemy3;

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

    public void choosenemys()
    {
        int enemy1 = dropdownEnemy1.value;
        int enemy2 = dropdownEnemy2.value;
        int enemy3 = dropdownEnemy3.value;
        switch (roomNum)
        {
            case 1: minimumE = 3; break;
            case 2: minimumE = 5; break;
            case 3: minimumE = 10; break;
            case 4: minimumE = 15; break;
            case 5: minimumE = 20; break;
            default: minimumE = 0; break;
        }


        int totalCost = enemy1 * EnemyCost1 + enemy2 * EnemyCost2 + enemy3 * EnemyCost3;

        if (totalCost < minimumE)
        {
            StartCoroutine(FlashUI());
            Debug.Log("Not enough points used! Need at least " + minimumE);
            return;
        }
        else
        {
            if (roomNum == 5)
            {
                enemyui.SetActive(false);
                roomNum = 1;
            }
            else
            {
                roomNum++;
            }

            EnemysEachRoom.AddConfig(enemy1, enemy2, enemy3);
        }
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

    private IEnumerator FlashUI()
    {
        int flashes = 3;
        float flashDuration = 0.2f;

        for (int i = 0; i < flashes; i++)
        {
            SetUIColor(Color.red);
            yield return new WaitForSeconds(flashDuration);

            SetUIColor(Color.white);
            yield return new WaitForSeconds(flashDuration);
        }
        SetUIColor(Color.white);
    }

    private void SetUIColor(Color color)
    {
        minEnScore.color = color;
        totaleEnScore.color = color;
        button.image.color = color;
    }

    public void hubworld()
    {
        room.placehubworldobj = true;

        minimumE = 0;
        if (random)
        {
            randomEnemies();
        }
        else 
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }


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

        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }

        room.minsize = 40;
        room.maxsize = 45;

        room.placNumObj = 4;
        changemap();
    }

    public void room3()
    {
        room.placehubworldobj = false;

        minimumE = 10;

        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }

        room.minsize = 45;
        room.maxsize = 50;

        room.placNumObj = 5;
        changemap();
    }

    public void room4()
    {
        room.placehubworldobj = false;

        minimumE = 15;

        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }

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

        if (random)
        {
            randomEnemies();
        }
        else
        {
            if (EnemysEachRoom.roomConfigs.Count > 0)
            {
                var config = EnemysEachRoom.roomConfigs[0];
                SetEnemies(config.enemy1, config.enemy2, config.enemy3);
                EnemysEachRoom.roomConfigs.RemoveAt(0);
            }
            else
            {
                Debug.LogWarning("No enemy configuration left in the list!");
                randomEnemies(); // just incase i dumb
            }
        }
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
