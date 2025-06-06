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

    //dificoulty
    private float diff;


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
        //SaveGame.Load();
        //InvokeRepeating(nameof(SaveGame.Save), 300f, 300f);
        
        //goes to hubworld
        hubworld();
    }
    private void Update()
    {

        //find smt better for this to place add
        diff = gameState.GetDifficultyMultiplier();
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!11

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
            case 1: minimumE = Mathf.RoundToInt(3 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 2: minimumE = Mathf.RoundToInt(5 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 3: minimumE = Mathf.RoundToInt(10 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 4: minimumE = Mathf.RoundToInt(15 * Mathf.Pow(1.1f, gameState.level) * diff); break;
            case 5: minimumE = Mathf.RoundToInt(20 * Mathf.Pow(1.1f, gameState.level) * diff    ); break;
            default: minimumE = Mathf.RoundToInt(0 * Mathf.Pow(1.1f, gameState.level) * diff); break;
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

        minimumE = Mathf.RoundToInt(3 * Mathf.Pow(1.1f, gameState.level) * diff);
        //if u make them random
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
        room.minsize = Mathf.RoundToInt(35 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(40 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(3 * Mathf.Pow(1.1f, gameState.level) * diff );
        changemap();
    }

    public void room2()
    {

        minimumE = Mathf.RoundToInt(5 * Mathf.Pow(1.1f, gameState.level) * diff);
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

        room.minsize = Mathf.RoundToInt(40 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(45 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(4 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }

    public void room3()
    {
        room.placehubworldobj = false;

        minimumE = Mathf.RoundToInt(10 * Mathf.Pow(1.1f, gameState.level) * diff);

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

        room.minsize = Mathf.RoundToInt(45 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(50 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(5 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }

    public void room4()
    {
        room.placehubworldobj = false;

        minimumE = Mathf.RoundToInt(15 * Mathf.Pow(1.1f, gameState.level) * diff);

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

        room.minsize = Mathf.RoundToInt(50 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.minsize = Mathf.RoundToInt(60 * Mathf.Pow(1.1f, gameState.level) * diff);

        room.placNumObj = Mathf.RoundToInt(6 * Mathf.Pow(1.1f, gameState.level) * diff);
        changemap();
    }
    public void bosroom()
    {
        room.placehubworldobj = false;

        //or 1 bos
        minimumE = Mathf.RoundToInt(20 * Mathf.Pow(1.1f, gameState.level) * diff);

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
        room.minsize =  Mathf.RoundToInt(100 * Mathf.Pow(1.1f, gameState.level) * diff);
        room.maxsize = Mathf.RoundToInt(120 * Mathf.Pow(1.1f, gameState.level) * diff  );

        room.placNumObj = Mathf.RoundToInt(12 * Mathf.Pow(1.1f, gameState.level) * diff);
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
