using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roomSelect : MonoBehaviour
{
    public GameObject map;
    public PathGrid grid;

    //see if its random and keep track of roomnum
    public bool random = true;
    int roomNum = 1;

    //min enemies and the enemy cost
    private int minimumE;
    private int EnemyCost1 = 1;
    private int EnemyCost2 = 2;
    private int EnemyCost3 = 3;

    //the ui
    public GameObject enemyui;
    public TextMeshProUGUI minEnScore;
    public TextMeshProUGUI totaleEnScore;
    public TextMeshProUGUI roomnum;
    public Button button;

    private int totalCost;

    //the amount of enemys enemies
    public TMP_Dropdown dropdownEnemy1;
    public TMP_Dropdown dropdownEnemy2;
    public TMP_Dropdown dropdownEnemy3;

    private int enemy1;
    private int enemy2;
    private int enemy3;

    private void Awake()
    {
        //loads the game autosave after 5 min every 5 min
        SaveGame.Load();
        InvokeRepeating(nameof(SaveGame.Save), 300f, 300f);
        
        //goes to hubworld
        hubworld();
    }
    private void Update()
    {

        showui();


        //if not all the objects spawn in the hubworld
        if (room.failsave)
        {
            changemap();
        }
    }
    //its a suprise
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

    private void showui()
    {
        switch (roomNum)
        {
            case 1: minimumE = 3 + (gameState.level * 1); break;
            case 2: minimumE = 5 + (gameState.level * 1); break;
            case 3: minimumE = 10 + (gameState.level * 2); break;
            case 4: minimumE = 15 + (gameState.level * 2); break;
            case 5: minimumE = 20 + (gameState.level * 3); break;
            default: minimumE = 0 + (gameState.level * 3); break;
        }

        enemy1 = dropdownEnemy1.value;
        enemy2 = dropdownEnemy2.value;
        enemy3 = dropdownEnemy3.value;

        totalCost = enemy1 * EnemyCost1 + enemy2 * EnemyCost2 + enemy3 * EnemyCost3;

        roomnum.text = $"Room Number: {roomNum}";
        totaleEnScore.text = $"Total Score: {totalCost}";
        minEnScore.text = $"Min Score {minimumE}";
    }

    //needs to choose a enemy for each room or it will be random
    public void choosenemys()
    {



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
                random = true;
            }
            else
            {
                roomNum++;
            }

            EnemysEachRoom.AddConfig(enemy1, enemy2, enemy3);
        }
    }

    //sets the enemys to room where it gets ussed to spawn them in grid
    public bool SetEnemies(int enemy1, int enemy2, int enemy3)
    {
        room.enemy1 = enemy1;
        room.enemy2 = enemy2;
        room.enemy3 = enemy3;

        return true;
    }

    //if u dont have enought enemys
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

    //the hubworld
    public void hubworld()
    {
        room.placehubworldobj = true;
        minimumE = 0;

        random = true;

        randomEnemies();

        //if u make them random
        roomNum = 1;


        room.minsize = 30;
        room.maxsize = 35;

        changemap();
    }


    //5 rooms yes i shoulda done it cleaner ut to late
    public void room1()
    {
        //better to call it where needed but like u dont lose to much
        SaveGame.Save();

        room.placehubworldobj = false;

        minimumE = 3;


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
        room.minsize = 35 + (gameState.level * 2);
        room.maxsize = 40 + (gameState.level * 2);

        room.placNumObj = 3 + (gameState.level * 1);
        changemap();
    }

    public void room2()
    {

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

        room.minsize = 40 + (gameState.level * 2);
        room.maxsize = 45 + (gameState.level * 2);

        room.placNumObj = 4 + (gameState.level * 1);
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

        room.minsize = 45 + (gameState.level * 4);
        room.maxsize = 50 + (gameState.level * 4);

        room.placNumObj = 5 + (gameState.level * 3);
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

        room.minsize = 50 + (gameState.level * 5);
        room.maxsize = 60 + (gameState.level * 5);

        room.placNumObj = 6 + (gameState.level * 4);
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
        room.minsize = 100 + (gameState.level * 7);
        room.maxsize = 120 + (gameState.level * 7);

        room.placNumObj = 12 + (gameState.level * 9);
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
